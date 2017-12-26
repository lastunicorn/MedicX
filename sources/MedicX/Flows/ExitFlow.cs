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

namespace DustInTheWind.MedicX.Flows
{
    internal class ExitFlow : IFlow
    {
        private readonly MedicXApplication medicXApplication;

        public ExitFlow(MedicXApplication medicXApplication)
        {
            if (medicXApplication == null) throw new ArgumentNullException(nameof(medicXApplication));
            this.medicXApplication = medicXApplication;
        }

        public void Run()
        {
            medicXApplication.Exit();
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("Bye!");
        }
    }
}