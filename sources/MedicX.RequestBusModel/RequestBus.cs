// MedicX
// Copyright (C) 2017-2018 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DustInTheWind.MedicX.RequestBusModel
{
    public class RequestBus
    {
        private readonly IRequestHandlerFactory requestHandlerFactory;
        private readonly Dictionary<Type, Type> handlers = new Dictionary<Type, Type>();

        public RequestBus()
        {
            requestHandlerFactory = new RequestHandlerFactory();
        }

        public RequestBus(IRequestHandlerFactory requestHandlerFactory)
        {
            this.requestHandlerFactory = requestHandlerFactory ?? throw new ArgumentNullException(nameof(requestHandlerFactory));
        }

        public void Register<TRequest, THandler>()
        {
            Type requestType = typeof(TRequest);
            Type requestHandlerType = typeof(THandler);

            if (handlers.ContainsKey(requestType))
                throw new Exception("The type " + requestType.FullName + " is already registered.");

            handlers.Add(requestType, requestHandlerType);
        }

        public async Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Type requestType = typeof(TRequest);

            if (!handlers.ContainsKey(requestType))
                throw new HandlerNotFoundException();  

            if (request is IValidatableObject validatableRequest)
                validatableRequest.Validate();

            Type requestHandlerType = handlers[requestType];

            if (requestHandlerFactory.Create(requestHandlerType) is IRequestHandler<TRequest, TResponse> requestHandlerWithResponse)
                return await requestHandlerWithResponse.Handle(request);

            if (requestHandlerFactory.Create(requestHandlerType) is IRequestHandler<TRequest> requestHandlerWithoutResponse)
            {
                await requestHandlerWithoutResponse.Handle(request);
                return default(TResponse);
            }

            throw new UnusableRequestHandlerException();
        }

        public async Task ProcessRequest<TRequest>(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Type requestType = typeof(TRequest);

            if (!handlers.ContainsKey(requestType))
                throw new HandlerNotFoundException();

            if (request is IValidatableObject validatableRequest)
                validatableRequest.Validate();

            Type requestHandlerType = handlers[requestType];

            if (requestHandlerFactory.Create(requestHandlerType) is IRequestHandler<TRequest> requestHandlerWithoutResponse)
                await requestHandlerWithoutResponse.Handle(request);
            else if (requestHandlerFactory.Create(requestHandlerType) is IRequestHandler<TRequest, object> requestHandlerWithResponse)
                await requestHandlerWithResponse.Handle(request);
            else throw new UnusableRequestHandlerException();
        }
    }
}
