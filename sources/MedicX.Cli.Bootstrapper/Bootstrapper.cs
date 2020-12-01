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

using System.Reflection;
using DustInTheWind.MedicX.CommandApplication;
using DustInTheWind.MedicX.CommandApplication.ApplicationInitialization;
using DustInTheWind.MedicX.RequestBusModel;
using MedicX.Cli.Presentation;
using Ninject;
using Ninject.Syntax;

namespace DustInTheWind.MedicX.Cli.Bootstrapper
{
    internal class Bootstrapper
    {
        public void Run()
        {
            IKernel kernel = new StandardKernel();

            ConfigureServices(kernel);
            ConfigureRequestBus(kernel);
            LoadProject(kernel);
            RunUiApplication(kernel);
        }

        private static void ConfigureServices(IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        private void ConfigureRequestBus(IResolutionRoot kernel)
        {
            RequestBus requestBus = kernel.Get<RequestBus>();
            RequestBusConfig.Configure(requestBus);
        }

        private void LoadProject(IResolutionRoot kernel)
        {
            RequestBus requestBus = kernel.Get<RequestBus>();
            ApplicationInitializationRequest request = new ApplicationInitializationRequest();

            requestBus.ProcessRequest(request).Wait();
        }

        private static void RunUiApplication(IResolutionRoot kernel)
        {
            MedicXApplication application = kernel.Get<MedicXApplication>();
            application.Run();
        }
    }
}