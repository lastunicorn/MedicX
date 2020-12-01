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
using MedicX.Cli.Presentation;
using Ninject;

namespace DustInTheWind.MedicX.Cli.Bootstrapper
{
    internal class CommandFactory : ICommandFactory
    {
        private readonly IKernel kernel;

        public CommandFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public ICommand Create(Type commandType)
        {
            if (!typeof(ICommand).IsAssignableFrom(commandType))
                throw new ArgumentException("The specified type is not a command.", nameof(commandType));

            return (ICommand)kernel.Get(commandType);
        }
    }
}