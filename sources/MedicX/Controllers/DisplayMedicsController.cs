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
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Cli.Controllers
{
    internal class DisplayMedicsController : IController
    {
        private readonly UnitOfWork unitOfWork;
        private readonly string searchText;

        public DisplayMedicsController(UnitOfWork unitOfWork, string searchText = null)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.searchText = searchText;
        }

        public void Run()
        {
            if (string.IsNullOrEmpty(searchText))
                DisplayAllMedics();
            else
                SearchMedic();
        }

        private void DisplayAllMedics()
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.GetAll();

            if (medics == null || medics.Count == 0)
                CustomConsole.WriteLineError("No medics exist in the database.");
            else
                DisplayMedicList(medics);
        }

        private static void DisplayMedicList(IEnumerable<Medic> medics)
        {
            Table medicsTable = CreateMedicsTable(medics);

            CustomConsole.WriteLine("Medics found: " + medicsTable.RowCount);

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            medicsTable.Render(tablePrinter);
        }

        private static Table CreateMedicsTable(IEnumerable<Medic> medics)
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

            foreach (Medic medic in medics)
            {
                medicsTable.AddRow(new[]
                {
                    new Cell(medic.Name),
                    new Cell(new MultilineText(medic.Specializations))
                });
            }

            return medicsTable;
        }

        private void SearchMedic()
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.Search(searchText);

            if (medics == null || medics.Count == 0)
                CustomConsole.WriteLineError("No medics exist in the database contining the searched text.");
            else if (medics.Count == 1)
                DisplayMedicDetails(medics[0]);
            else
                DisplayMedicList(medics);
        }

        private static void DisplayMedicDetails(Medic medic)
        {
            Table medicTable = CreateMedicDetailsTable(medic);

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            medicTable.Render(tablePrinter);
        }

        private static Table CreateMedicDetailsTable(Medic medic)
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
                new Cell(medic.Name)
            });

            if (medic.Specializations?.Count > 0)
                medicsTable.AddRow(new[]
                {
                    new Cell("Specializations"),
                    new Cell(new MultilineText(medic.Specializations))
                });

            if (medic.Comments != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Comments"),
                    new Cell(medic.Comments)
                });

            return medicsTable;
        }
    }
}