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
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.GuiApplication.GetAllClinics;
using DustInTheWind.MedicX.GuiApplication.GetAllMedics;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.GuiApplication.SetCurrentItem
{
    internal class SetCurrentItemRequestHandler : IRequestHandler<SetCurrentItemRequest>
    {
        private readonly ProjectRepository projectRepository;

        public SetCurrentItemRequestHandler(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public Task Handle(SetCurrentItemRequest request)
        {
            return Task.Run(() =>
            {
                Project currentProject = projectRepository.ActiveProject;

                if (currentProject == null)
                    throw new NoProjectException();

                object newCurrentItem = CalculateNewCurrentItem(currentProject, request);

                currentProject.CurrentItem = newCurrentItem;
            });
        }

        private static object CalculateNewCurrentItem(Project currentProject, SetCurrentItemRequest request)
        {
            using (IUnitOfWork unitOfWork = currentProject.CreateUnitOfWork())
            {
                switch (request.NewCurrentItem)
                {
                    case MedicDto medic:
                        return unitOfWork.MedicRepository.GetById(medic.Id);

                    case ClinicDto clinic:
                        return unitOfWork.ClinicRepository.GetById(clinic.Id);

                    case Consultation consultation:
                        return unitOfWork.ConsultationRepository.GetById(consultation.Id);

                    case Investigation investigation:
                        return unitOfWork.InvestigationRepository.GetById(investigation.Id);

                    default:
                        return null;
                }
            }
        }
    }
}