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
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools;

namespace MedicX.Cli.Presentation
{
    public class ControllerPool
    {
        private readonly ICommandFactory commandFactory;
        private List<Type> commandTypes;

        public ControllerPool(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

            CreateCommands();
        }

        private void CreateCommands()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            commandTypes = assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && typeof(ICommand).IsAssignableFrom(x) && x.GetCustomAttribute<CommandAttribute>() != null)
                .ToList();
        }

        public ICommand Get(UserCommand command)
        {
            Type commandType = commandTypes.FirstOrDefault(x =>
            {
                CommandAttribute commandAttribute = x.GetCustomAttribute<CommandAttribute>();
                return commandAttribute != null && commandAttribute.IsMatch(command.Name);
            });

            if (commandType == null)
                return null;

            return commandFactory.Create(commandType);
        }
    }
}