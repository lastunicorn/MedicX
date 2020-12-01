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
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.GuiApplication.GetAllClinics
{
    internal class GetAllClinicsRequestHandler : IRequestHandler<GetAllClinicsRequest, List<ClinicDto>>
    {
        private readonly ProjectRepository projectRepository;

        public GetAllClinicsRequestHandler(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public Task<List<ClinicDto>> Handle(GetAllClinicsRequest request)
        {
            Project currentProject = projectRepository.ActiveProject;

            if (currentProject == null)
                throw new NoProjectException();

            using (IUnitOfWork unitOfWork = currentProject.CreateUnitOfWork())
            {
                List<ClinicDto> clinics = unitOfWork.ClinicRepository.GetAll()
                    .Select(x => new ClinicDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = new Address(x.Address),
                        Program = x.Program,
                        Comments = x.Comments
                    })
                    .ToList();

                return Task.FromResult(clinics);
            }
        }
    }
}