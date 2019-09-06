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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Common;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.LoadProject
{
    public class LoadProjectRequestHandler : IRequestHandler<LoadProjectRequest, MedicXProject>
    {
        private readonly MedicXApplication medicXApplication;

        public LoadProjectRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task<MedicXProject> Handle(LoadProjectRequest request)
        {
            return Task.Run(() =>
            {
                medicXApplication.LoadProject(request.FileName);
                return medicXApplication.CurrentProject;
            });
        }
    }
}