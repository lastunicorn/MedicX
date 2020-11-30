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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.AddNewConsultation
{
    internal class AddNewConsultationRequestHandler : IRequestHandler<AddNewConsultationRequest>
    {
        private readonly MedicXApplication medicXApplication;

        public AddNewConsultationRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task Handle(AddNewConsultationRequest request)
        {
            Project currentProject = medicXApplication.CurrentProject;

            if (currentProject == null)
                throw new NoProjectException();

            using (IUnitOfWork unitOfWork = currentProject.CreateUnitOfWork())
            {
                Consultation consultation = new Consultation
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Today,
                    Labels = new ObservableCollection<string>()
                };

                unitOfWork.ConsultationRepository.AddOrUpdate(consultation);
                currentProject.CurrentItem = consultation;
            }

            return Task.CompletedTask;
        }
    }
}