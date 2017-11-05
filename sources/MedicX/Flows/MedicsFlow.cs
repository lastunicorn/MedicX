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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;
using DustInTheWind.MedicX.TableDisplay;

namespace DustInTheWind.MedicX.Flows
{
    internal class MedicsFlow : IFlow
    {
        private readonly UnitOfWork unitOfWork;

        public MedicsFlow(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            this.unitOfWork = unitOfWork;
        }

        public void Run()
        {
            DisplayMedics();
        }

        private void DisplayMedics()
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.GetAll();

            if (medics != null && medics.Any())
            {
                Table medicsTable = CreateMedicsTable(medics);

                ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
                medicsTable.Render(tablePrinter);
            }
            else
            {
                Console.WriteLine("No medics exist in the database.");
            }
        }

        private static Table CreateMedicsTable(IEnumerable<Medic> medics)
        {
            Table medicsTable = new Table
            {
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
    }
}