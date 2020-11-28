using System.Collections.Generic;
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Domain.Entities;
using MedicX.Cli.Presentation.UserControls;

namespace MedicX.Cli.Presentation.Views
{
    internal class DisplayMedicsView
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