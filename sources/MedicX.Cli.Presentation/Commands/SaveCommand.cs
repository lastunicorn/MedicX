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
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Domain.DataAccess;

namespace MedicX.Cli.Presentation.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly IUnitOfWork unitOfWork;

        public SaveCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public bool IsMatch(UserCommand command)
        {
            return command.Name == "save";
        }

        public void Execute(UserCommand command)
        {
            unitOfWork.Save();
            CustomConsole.WriteLineSuccess("Changes were successfully saved.");
        }
    }
}