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
using MedicX.Cli.Presentation.UserControls;

namespace MedicX.Cli.Presentation
{
    public class MedicXApplication
    {
        private readonly CommandPool commandPool;
        private readonly Prompter prompter;

        public MedicXApplication(CommandPool commandPool)
        {
            this.commandPool = commandPool ?? throw new ArgumentNullException(nameof(commandPool));

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
            ICommand command = commandPool.Get(e.Command);

            if (command == null)
            {
                CustomConsole.WriteLineError("Unknown command");
            }
            else
            {
                try
                {
                    command.Execute(e.Command);
                }
                catch (Exception ex)
                {
#if DEBUG
                    CustomConsole.WriteError(ex);
#else
                    CustomConsole.WriteError(ex.Message);
#endif
                }
            }

            CustomConsole.WriteLine();
        }

        public void Exit()
        {
            prompter.RequestExit();
        }
    }
}