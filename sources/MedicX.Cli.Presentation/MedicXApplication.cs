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
using MedicX.Cli.Presentation.UserControls;

namespace MedicX.Cli.Presentation
{
    public class MedicXApplication
    {
        private readonly ControllerPool controllerPool;
        private readonly Prompter prompter;

        public MedicXApplication(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            controllerPool = new ControllerPool(unitOfWork, this);
            prompter = new Prompter();
        }

        public void Run()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader();
            applicationHeader.Display();

            prompter.NewCommand += HandleNewCommand;

            try
            {
                prompter.WaitForUserCommand();
            }
            finally
            {
                prompter.NewCommand -= HandleNewCommand;
            }
        }

        private void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            ICommand command = controllerPool.Get(e.Command);

            if (command == null)
                CustomConsole.WriteLineError("Unknown command");
            else
                command.Execute(e.Command);

            CustomConsole.WriteLine();
        }

        public void Exit()
        {
            prompter.RequestExit();
        }
    }
}