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

namespace DustInTheWind.MedicX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    DisplayMedics(unitOfWork);

                    IConsultationsRepository consultationsRepository = unitOfWork.ConsultationsRepository;

                    List<Consultation> consultations = consultationsRepository.GetAll();

                    foreach (Consultation consultation in consultations)
                    {
                        Console.WriteLine();
                        Console.WriteLine("{0:yyyy-MM-dd} - {1}", consultation.Date, consultation.Medic.Name);
                        Console.WriteLine("Comments: {0}", consultation.Comments);
                    }

                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }

            Pause();
        }

        private static void DisplayMedics(UnitOfWork unitOfWork)
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.GetAll();

            if (medics != null && medics.Any())
            {
                Console.WriteLine("The list of medics:");

                foreach (Medic medic in medics)
                    Console.WriteLine($"- {medic.Name}");
            }
            else
            {
                Console.WriteLine("No medics exist.");
            }
        }

        private static void DisplayError(Exception ex)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = oldColor;
        }

        private static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}