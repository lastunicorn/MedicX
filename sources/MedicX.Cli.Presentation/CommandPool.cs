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
    public class CommandPool
    {
        private readonly ICommandFactory commandFactory;
        private readonly List<Type> commandTypes;

        public CommandPool(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

            commandTypes = FindCommandTypes();
        }

        private static List<Type> FindCommandTypes()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && typeof(ICommand).IsAssignableFrom(x) && x.GetCustomAttribute<CommandAttribute>() != null)
                .ToList();
        }

        public ICommand Get(UserCommand userCommand)
        {
            Type commandType = MatchCommandType(userCommand);

            if (commandType == null)
                return null;

            return CreateCommandInstance(commandType, userCommand);
        }

        private Type MatchCommandType(UserCommand command)
        {
            return commandTypes.FirstOrDefault(x =>
            {
                CommandAttribute commandAttribute = x.GetCustomAttribute<CommandAttribute>();
                return commandAttribute != null && commandAttribute.IsMatch(command);
            });
        }

        private ICommand CreateCommandInstance(Type commandType, UserCommand userCommand)
        {
            ICommand command = commandFactory.Create(commandType);
            PopulateProperties(command, userCommand);

            return command;
        }

        private static void PopulateProperties(ICommand command, UserCommand userCommand)
        {
            IEnumerable<PropertyInfo> propertyInfos = command.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                CommandPropertyAttribute attribute = propertyInfo.GetCustomAttribute<CommandPropertyAttribute>();
                UserCommandParameter parameter = userCommand.Parameters.FirstOrDefault(x => x.Name == attribute.Name);

                if (parameter != null)
                    propertyInfo.SetValue(command, parameter.Value);
            }
        }
    }
}