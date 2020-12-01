﻿// MedicX
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
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.GuiApplication.AddNewEmptyClinic
{
    internal class AddNewEmptyClinicRequestHandler : IRequestHandler<AddNewEmptyClinicRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddNewEmptyClinicRequestHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task Handle(AddNewEmptyClinicRequest request)
        {
            Clinic clinic = new Clinic();
            unitOfWork.ClinicRepository.AddOrUpdate(clinic);

            return Task.CompletedTask;
        }
    }

    //internal class AddNewClinicRequestHandler : IRequestHandler<AddNewClinicRequest>
    //{
    //    private readonly MedicXApplication medicXApplication;

    //    public AddNewClinicRequestHandler(MedicXApplication medicXApplication)
    //    {
    //        this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
    //    }

    //    public Task Handle(AddNewClinicRequest request)
    //    {
    //        MedicXProject currentProject = medicXApplication.CurrentProject;

    //        if (currentProject != null)
    //        {
    //            Clinic clinic = currentProject.Clinics.AddNew();
    //            currentProject.CurrentItem = clinic;
    //        }

    //        return Task.CompletedTask;
    //    }
    //}
}