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
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.SaveProject
{
    public class SaveProjectRequestHandler : IRequestHandler<SaveProjectRequest>
    {
        private readonly MedicXApplication medicXApplication;

        public SaveProjectRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task Handle(SaveProjectRequest request)
        {
            return Task.Run(() =>
            {
                medicXApplication.CurrentProject?.Save();
            });
        }
    }
}
