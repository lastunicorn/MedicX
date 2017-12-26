// MedicX
// Copyright (C) 2017 Dust in the Wind
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
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX
{
    internal class MedicXApplication
    {
        private readonly FlowPool flowPool;
        private readonly Prompter prompter;

        public MedicXApplication(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            flowPool = new FlowPool(unitOfWork, this);
            prompter = new Prompter();
        }

        public void Run()
        {
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
            IFlow flow = flowPool.Get(e.Command);

            if (flow == null)
                CustomConsole.WriteLineError("Unknown command");
            else
                flow.Run();

            CustomConsole.WriteLine();
        }

        public void Exit()
        {
            prompter.RequestExit();
        }
    }
}