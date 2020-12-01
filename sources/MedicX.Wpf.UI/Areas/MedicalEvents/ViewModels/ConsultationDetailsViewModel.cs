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
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.GuiApplication.GetAllClinics;
using DustInTheWind.MedicX.GuiApplication.GetAllMedics;
using DustInTheWind.MedicX.GuiApplication.UpdateConsultationClinic;
using DustInTheWind.MedicX.RequestBusModel;
using Medic = DustInTheWind.MedicX.Domain.Entities.Medic;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels
{
    internal class ConsultationDetailsViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private string title;
        private List<Medic> medics;
        private List<ClinicDto> clinics;
        private ClinicDto selectedClinic;
        private bool isClinicsEnabled;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private Consultation Consultation { get; }

        public List<Medic> Medics
        {
            get => medics;
            private set
            {
                medics = value;
                OnPropertyChanged();
            }
        }

        public List<ClinicDto> Clinics
        {
            get => clinics;
            private set
            {
                clinics = value;
                OnPropertyChanged();
            }
        }

        public ClinicDto SelectedClinic
        {
            get => selectedClinic;
            set
            {
                selectedClinic = value;
                OnPropertyChanged();

                IsClinicsEnabled = false;

                UpdateConsultationSetClinicRequest request = new UpdateConsultationSetClinicRequest
                {
                    ConsultationId = Consultation.Id,
                    ClinicId = value.Id
                };

                requestBus.ProcessRequest(request)
                    .ContinueWith(t =>
                    {
                        if (t.Exception != null)
                            SelectedClinic = ClinicDto.Empty;
                        else
                            IsClinicsEnabled = true;
                    });
            }
        }

        public bool IsClinicsEnabled
        {
            get => isClinicsEnabled;
            private set
            {
                isClinicsEnabled = value;
                OnPropertyChanged();
            }
        }

        public ConsultationDetailsViewModel(Consultation consultation, RequestBus requestBus)
        {
            Consultation = consultation ?? throw new ArgumentNullException(nameof(consultation));
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

            Consultation.DateChanged += HandleConsultationDateChanged;
            Consultation.MedicChanged += HandleConsultationMedicChanged;

            PopulateMedics();
            PopulateClinics();

            UpdateTitle();
        }

        private void UpdateSelectedClinic()
        {
            if (Consultation.Clinic == null)
                SelectedClinic = ClinicDto.Empty;
            else
            {
                ClinicDto clinic = Clinics.FirstOrDefault(x => x.Id == Consultation.Clinic.Id);
                SelectedClinic = clinic;
            }
        }

        private void PopulateMedics()
        {
            GetAllMedicsRequest request = new GetAllMedicsRequest();

            requestBus.ProcessRequest<GetAllMedicsRequest, List<Medic>>(request)
                .ContinueWith(t =>
                {
                    Medics = t.Result
                        .OrderBy(x => x.Name)
                        .ToList();
                });
        }

        private void PopulateClinics()
        {
            IsClinicsEnabled = false;

            GetAllClinicsRequest request = new GetAllClinicsRequest();

            requestBus.ProcessRequest<GetAllClinicsRequest, List<ClinicDto>>(request)
                .ContinueWith(t =>
                {
                    Clinics = t.Result
                        .OrderBy(x => x.Name)
                        .ToList();

                    IsClinicsEnabled = true;

                    UpdateSelectedClinic();
                });
        }

        private void HandleConsultationDateChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void HandleConsultationMedicChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            Title = string.Format("{0:yyyy MM dd} - {1} - (consultation)", Consultation.Date, Consultation.Medic?.Name);
        }
    }
}