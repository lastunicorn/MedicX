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
using DustInTheWind.MedicX.Utils;

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
                    DisplayConsultations(unitOfWork);

                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
               CustomConsole.WriteError(ex);
            }

            CustomConsole.Pause();
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

        private static void DisplayConsultations(UnitOfWork unitOfWork)
        {
            IConsultationsRepository consultationsRepository = unitOfWork.ConsultationsRepository;

            List<Consultation> consultations = consultationsRepository.GetAll();

            foreach (Consultation consultation in consultations)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLineEmphasies("{0:yyyy-MM-dd} - {1}", consultation.Date, consultation.Medic.Name);
                CustomConsole.WriteLine("Comments: {0}", consultation.Comments);
                CustomConsole.WriteLine("Prescriptions:");

                foreach (Prescription prescription in consultation.Prescriptions)
                    CustomConsole.WriteLine("- {0}", prescription.Description);
            }
        }
    }
}