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

namespace DustInTheWind.MedicX.GuiApplication.AddNewClinic
{
    internal class AddNewClinicRequestHandler : IRequestHandler<AddNewClinicRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddNewClinicRequestHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task Handle(AddNewClinicRequest request)
        {
            Clinic clinic = new Clinic
            {
                Name = request.Name,
                Address = request.Address,
                Phones = new ObservableCollection<string>(request.Phones),
                Program = request.Program
            };
            unitOfWork.ClinicRepository.AddOrUpdate(clinic);

            return Task.CompletedTask;
        }
    }
}