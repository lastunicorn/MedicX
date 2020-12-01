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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.CommandApplication.AddMedic
{
    public class AddMedicRequestHandler : IRequestHandler<AddMedicRequest>
    {
        private readonly ProjectRepository projectRepository;
        private readonly IMedicView medicView;

        public AddMedicRequestHandler(ProjectRepository projectRepository, IMedicView medicView)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.medicView = medicView ?? throw new ArgumentNullException(nameof(medicView));
        }

        public Task Handle(AddMedicRequest request)
        {
            return Task.Run(() =>
            {
                projectRepository.RunWithUnitOfWork(unitOfWork =>
                {
                    Medic medic = new Medic
                    {
                        Name = request.Name ?? medicView.ReadName(),
                        Specializations = new ObservableCollection<string>(request.Specializations ?? medicView.ReadSpecializations()),
                        Comments = request.Comments ?? medicView.ReadComments()
                    };

                    unitOfWork.MedicRepository.Add(medic);
                    unitOfWork.Save();
                });
            });
        }
    }
}