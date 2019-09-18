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
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using ClinicDto = DustInTheWind.MedicX.Application.GetAllClinics.Clinic;
using MedicDto = DustInTheWind.MedicX.Application.GetAllMedics.Medic;

namespace DustInTheWind.MedicX.Application.SetCurrentItem
{
    internal class SetCurrentItemRequestHandler : IRequestHandler<SetCurrentItemRequest>
    {
        private readonly MedicXApplication medicXApplication;

        public SetCurrentItemRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task Handle(SetCurrentItemRequest request)
        {
            return Task.Run(() =>
            {
                MedicXProject currentProject = medicXApplication.CurrentProject;
                object newCurrentItem = CalculateNewCurrentItem(currentProject, request);

                currentProject.CurrentItem = newCurrentItem;
            });
        }

        private static object CalculateNewCurrentItem(MedicXProject currentProject, SetCurrentItemRequest request)
        {
            switch (request.NewCurrentItem)
            {
                case MedicDto medic:
                    return currentProject.Medics.FirstOrDefault(x => x.Id == medic.Id);

                case ClinicDto clinic:
                    return currentProject.Clinics.FirstOrDefault(x => x.Id == clinic.Id);

                case MedicalEvent medicalEvent:
                    return currentProject.MedicalEvents.FirstOrDefault(x => x.Id == medicalEvent.Id);

                default:
                    return null;
            }
        }
    }
}