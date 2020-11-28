using System.Collections.Generic;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.MedicX.Domain.Entities;

namespace MedicX.Cli.Presentation.UserControls
{
    internal class MedicsTableControl
    {
        public List<Medic> Medics { get; set; }

        public void Display()
        {
            if (Medics == null)
                return;

            Table medicsTable = CreateMedicsTable();

            CustomConsole.WriteLine("Medics found: " + medicsTable.RowCount);

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            medicsTable.Render(tablePrinter);
        }

        private Table CreateMedicsTable()
        {
            Table medicsTable = new Table
            {
                Border = TableBorder.SingleLineBorder,
                DrawLinesBetweenRows = true,
                DisplayColumnHeaders = true
            };

            medicsTable.Columns.Add(new Column("Name")
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Header = new MultilineText("Name")
            });

            medicsTable.Columns.Add(new Column("Specializations")
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Header = new MultilineText("Specializations")
            });

            foreach (Medic medic in Medics)
            {
                medicsTable.AddRow(new[]
                {
                    new Cell(medic.Name),
                    new Cell(new MultilineText(medic.Specializations))
                });
            }

            return medicsTable;
        }
    }
}