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

using DustInTheWind.MedicX.CommandApplication.AddMedic;
using DustInTheWind.MedicX.CommandApplication.ApplicationInitialization;
using DustInTheWind.MedicX.CommandApplication.SearchMedic;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.CommandApplication
{
    public class RequestBusConfig
    {
        public static void Configure(RequestBus requestBus)
        {
            requestBus.Register<ApplicationInitializationRequest, ApplicationInitializationRequestHandler>();
            requestBus.Register<AddMedicRequest, AddMedicRequestHandler>();
            requestBus.Register<SearchMedicRequest, SearchMedicRequestHandler>();
        }
    }
}