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
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.Persistence;

namespace DustInTheWind.MedicX.Cli.Controllers
{
    internal class ConsultationsController : IController
    {
        private readonly UnitOfWork unitOfWork;
        private readonly string searchText;
        private readonly TextOutputControl textOutputControl;
        private readonly ListOutputControl listOutputControl;

        public ConsultationsController(UnitOfWork unitOfWork, string searchText = null)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.searchText = searchText;

            textOutputControl = new TextOutputControl();
            listOutputControl = new ListOutputControl
            {
                ItemsIndentation = 4
            };
        }

        public void Run()
        {
            if (string.IsNullOrEmpty(searchText))
                DisplayAllConsultations();
            else
                SearchConsultation();
        }

        private void DisplayAllConsultations()
        {
            IConsultationRepository consultationRepository = unitOfWork.ConsultationRepository;

            List<Consultation> consultations = consultationRepository.GetAll();

            if (consultations == null || consultations.Count == 0)
                CustomConsole.WriteLineError("No consultations exist in the database.");
            else
                DisplayConsultations(consultations);
        }

        private void SearchConsultation()
        {
            IConsultationRepository consultationRepository = unitOfWork.ConsultationRepository;

            List<Consultation> consultations = consultationRepository.Search(searchText);

            if (consultations == null || consultations.Count == 0)
                CustomConsole.WriteLineError("No consultations exist in the database contining the searched text.");
            else
                DisplayConsultations(consultations);
        }

        private void DisplayConsultations(IEnumerable<Consultation> consultations)
        {
            bool isFirstItem = true;

            foreach (Consultation consultation in consultations)
            {
                if (!isFirstItem)
                {
                    CustomConsole.WriteLine();
                    CustomConsole.WriteLine("-------------------------------------------------------------------------------");
                    CustomConsole.WriteLine();
                }

                DisplayConsultation(consultation);

                isFirstItem = false;
            }
        }

        private void DisplayConsultation(Consultation consultation)
        {
            CustomConsole.WriteLineEmphasies("{0:yyyy MM dd} > {1}", consultation.Date, consultation.Medic.Name);

            if (consultation.Clinic != null)
                textOutputControl.Write("Clinic", consultation.Clinic.Name);

            if (consultation.Labels != null && consultation.Labels.Count > 0)
                listOutputControl.Write("Labels", consultation.Labels);

            if (consultation.Comments != null)
                textOutputControl.Write("Comments", consultation.Comments);

            if (consultation.Prescriptions != null && consultation.Prescriptions.Count > 0)
                listOutputControl.Write("Prescriptions", consultation.Prescriptions.Select(x => x.Description));
        }
    }
}