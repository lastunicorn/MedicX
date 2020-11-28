using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.MedicX.Domain.Entities;

namespace MedicX.Cli.Presentation.UserControls
{
    internal class MedicDetailsControl
    {
        public Medic Medic { get; set; }

        public void Display()
        {
            if (Medic == null)
                return;

            Table medicTable = CreateMedicDetailsTable();

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            medicTable.Render(tablePrinter);
        }

        private Table CreateMedicDetailsTable()
        {
            Table medicsTable = new Table
            {
                Border = TableBorder.SingleLineBorder,
                DrawLinesBetweenRows = true,
                DisplayColumnHeaders = false
            };

            medicsTable.Columns.Add(new Column("Field")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            });

            medicsTable.Columns.Add(new Column("Value")
            {
                HorizontalAlignment = HorizontalAlignment.Left
            });

            medicsTable.AddRow(new[]
            {
                new Cell("Name"),
                new Cell(Medic.Name)
            });

            if (Medic.Specializations?.Count > 0)
                medicsTable.AddRow(new[]
                {
                    new Cell("Specializations"),
                    new Cell(new MultilineText(Medic.Specializations))
                });

            if (Medic.Comments != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Comments"),
                    new Cell(Medic.Comments)
                });

            return medicsTable;
        }
    }
}