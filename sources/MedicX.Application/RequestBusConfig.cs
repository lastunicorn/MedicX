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

using DustInTheWind.MedicX.Application.ExitApplication;
using DustInTheWind.MedicX.Application.GetAllClinics;
using DustInTheWind.MedicX.Application.GetAllMedics;
using DustInTheWind.MedicX.Application.GetCurrentProject;
using DustInTheWind.MedicX.Application.GetCurrentProjectStatus;
using DustInTheWind.MedicX.Application.LoadProject;
using DustInTheWind.MedicX.Application.SaveProject;
using DustInTheWind.MedicX.Application.SetCurrentItem;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application
{
    public class RequestBusConfig
    {
        public static void Configure(RequestBus requestBus)
        {
            requestBus.Register<LoadProjectRequest, LoadProjectRequestHandler>();
            requestBus.Register<SaveProjectRequest, SaveProjectRequestHandler>();
            requestBus.Register<GetCurrentProjectRequest, GetCurrentProjectRequestHandler>();
            requestBus.Register<GetCurrentProjectStatusRequest, GetCurrentProjectStatusRequestHandler>();
            requestBus.Register<ExitApplicationRequest, ExitApplicationRequestHandler>();
            requestBus.Register<SetCurrentItemRequest, SetCurrentItemRequestHandler>();
            requestBus.Register<GetAllMedicsRequest, GetAllMedicsRequestHandler>();
            requestBus.Register<GetAllClinicsRequest, GetAllClinicsRequestHandler>();
        }
    }
}