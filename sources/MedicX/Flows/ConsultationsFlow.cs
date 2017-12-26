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
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Flows
{
    internal class ConsultationsFlow : IFlow
    {
        private readonly UnitOfWork unitOfWork;

        public ConsultationsFlow(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            this.unitOfWork = unitOfWork;
        }

        public void Run()
        {
            DisplayConsultations();
        }

        private void DisplayConsultations()
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