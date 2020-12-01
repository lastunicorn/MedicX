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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.CommandApplication.SearchMedic;
using DustInTheWind.MedicX.Domain.Entities;
using MedicX.Cli.Presentation.UserControls;

namespace MedicX.Cli.Presentation.Views
{
    public class DisplayMedicsView : IDisplayMedicsView
    {
        public void DisplayMedics(List<Medic> medics)
        {
            if (medics == null || medics.Count == 0)
                CustomConsole.WriteLineError("<No medics>");
            else if (medics.Count == 1)
                DisplayMedicDetails(medics[0]);
            else
                DisplayMedicList(medics);
        }

        private static void DisplayMedicDetails(Medic medic)
        {
            MedicDetailsControl medicDetailsControl = new MedicDetailsControl
            {
                Medic = medic
            };

            medicDetailsControl.Display();
        }

        private static void DisplayMedicList(List<Medic> medics)
        {
            MedicsTableControl medicsTableControl = new MedicsTableControl
            {
                Medics = medics
            };

            medicsTableControl.Display();
        }
    }
}