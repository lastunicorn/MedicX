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
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Domain.DataAccess;
using MedicX.Cli.Presentation.Commands;
using MedicX.Cli.Presentation.Views;

namespace MedicX.Cli.Presentation
{
    internal class ControllerPool
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MedicXApplication medicXApplication;
        private List<ICommand> controllers;

        public ControllerPool(IUnitOfWork unitOfWork, MedicXApplication medicXApplication)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));

            CreateControllers();
        }

        private void CreateControllers()
        {
            controllers = new List<ICommand>
            {
                new AddMedicCommand(unitOfWork),
                new DisplayMedicsCommand(unitOfWork, new DisplayMedicsView()),
                new DisplayClinicsCommand(unitOfWork),
                new ConsultationsCommand(unitOfWork),
                new SaveCommand(unitOfWork),
                new ExitCommand(medicXApplication),
                new HelpCommand()
            };
        }

        public ICommand Get(UserCommand command)
        {
            return controllers.FirstOrDefault(x => x.IsMatch(command));
        }
    }
}