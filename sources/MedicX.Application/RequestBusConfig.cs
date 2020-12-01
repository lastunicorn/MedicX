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

using DustInTheWind.MedicX.GuiApplication.AddNewConsultation;
using DustInTheWind.MedicX.GuiApplication.AddNewEmptyClinic;
using DustInTheWind.MedicX.GuiApplication.AddNewInvestigation;
using DustInTheWind.MedicX.GuiApplication.AddNewMedic;
using DustInTheWind.MedicX.GuiApplication.ApplicationInitialization;
using DustInTheWind.MedicX.GuiApplication.ExitApplication;
using DustInTheWind.MedicX.GuiApplication.GetAllClinics;
using DustInTheWind.MedicX.GuiApplication.GetAllMedics;
using DustInTheWind.MedicX.GuiApplication.GetCurrentItem;
using DustInTheWind.MedicX.GuiApplication.GetCurrentProject;
using DustInTheWind.MedicX.GuiApplication.SetCurrentItem;
using DustInTheWind.MedicX.GuiApplication.SetMedicAsCurrent;
using DustInTheWind.MedicX.GuiApplication.UpdateConsultationClinic;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.GuiApplication
{
    public class RequestBusConfig
    {
        public static void Configure(RequestBus requestBus)
        {
            requestBus.Register<ApplicationInitializationRequest, ApplicationInitializationRequestHandler>();
            requestBus.Register<GetCurrentProjectRequest, GetCurrentProjectRequestHandler>();
            requestBus.Register<ExitApplicationRequest, ExitApplicationRequestHandler>();
            requestBus.Register<SetCurrentItemRequest, SetCurrentItemRequestHandler>();
            requestBus.Register<GetAllMedicsRequest, GetAllMedicsRequestHandler>();
            requestBus.Register<GetAllClinicsRequest, GetAllClinicsRequestHandler>();
            requestBus.Register<AddNewMedicRequest, AddNewMedicRequestHandler>();
            requestBus.Register<AddNewEmptyClinicRequest, AddNewEmptyClinicRequestHandler>();
            requestBus.Register<AddNewInvestigationRequest, AddNewInvestigationRequestHandler>();
            requestBus.Register<AddNewConsultationRequest, AddNewConsultationRequestHandler>();
            requestBus.Register<UpdateConsultationSetClinicRequest, UpdateConsultationSetClinicRequestHandler>();
            requestBus.Register<GetCurrentItemRequest, GetCurrentItemRequestHandler>();
            requestBus.Register<SetMedicAsCurrentRequest, SetMedicAsCurrentRequestHandler>();
        }
    }
}