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
using System.Reflection;
using System.Threading;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;
using DustInTheWind.MedicX.TableDisplay;
using DustInTheWind.MedicX.Utils;

namespace DustInTheWind.MedicX
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DisplayAppHeader();

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    bool exitRequested = false;

                    while (!exitRequested)
                    {
                        Console.WriteLine();
                        Console.Write("> ");
                        string command = Console.ReadLine();

                        switch (command)
                        {
                            case "medic":
                                CustomConsole.WriteLine();
                                DisplayMedics(unitOfWork);
                                break;

                            case "consult":
                            case "consultation":
                                CustomConsole.WriteLine();
                                DisplayConsultations(unitOfWork);
                                break;

                            case "save":
                                unitOfWork.Save();
                                CustomConsole.WriteLineSuccess("Changes were successfully saved.");
                                break;

                            case "exit":
                            case "quit":
                                exitRequested = true;
                                CustomConsole.WriteLine();
                                CustomConsole.WriteLine("Bye!");
                                break;

                            case "help":
                                CustomConsole.WriteLine();
                                CustomConsole.WriteEmphasies("Commands: ");
                                CustomConsole.WriteLine("medic, consultation, save, exit, help");
                                break;

                            default:
                                CustomConsole.WriteLineError("Unknown command");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
                CustomConsole.Pause();
            }

            Thread.Sleep(300);
        }

        private static void DisplayAppHeader()
        {
            CustomConsole.WriteLine("MedicX " + Assembly.GetEntryAssembly().GetName().Version.ToString(3));
        }

        private static void DisplayMedics(UnitOfWork unitOfWork)
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.GetAll();

            if (medics != null && medics.Any())
            {
                Table medicsTable = CreateMedicsTable(medics);
                Console.WriteLine(medicsTable);
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
                Title = new MultilineText("Name")
            });

            medicsTable.Columns.Add(new Column("Specializations")
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Title = new MultilineText("Specializations")
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

        private static void DisplayConsultations(UnitOfWork unitOfWork)
        {
            IConsultationsRepository consultationsRepository = unitOfWork.ConsultationsRepository;

            List<Consultation> consultations = consultationsRepository.GetAll();

            bool isFirstItem = true;

            foreach (Consultation consultation in consultations)
            {
                if (!isFirstItem)
                    CustomConsole.WriteLine();

                CustomConsole.WriteLineEmphasies("{0:yyyy-MM-dd} - {1}", consultation.Date, consultation.Medic.Name);
                CustomConsole.WriteLine("Comments: {0}", consultation.Comments);
                CustomConsole.WriteLine("Prescriptions:");

                foreach (Prescription prescription in consultation.Prescriptions)
                    CustomConsole.WriteLine("- {0}", prescription.Description);

                isFirstItem = false;
            }
        }
    }
}