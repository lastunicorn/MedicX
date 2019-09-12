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
using DustInTheWind.MedicX.Application.GetAllClinics;
using DustInTheWind.MedicX.Application.GetAllMedics;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels
{
    internal class InvestigationDetailsViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private string title;
        private List<Medic> medics;
        private List<Clinic> clinics;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public Investigation Investigation { get; }

        public List<Medic> Medics
        {
            get => medics;
            private set
            {
                medics = value;
                OnPropertyChanged();
            }
        }

        public List<Clinic> Clinics
        {
            get => clinics;
            private set
            {
                clinics = value;
                OnPropertyChanged();
            }
        }

        public InvestigationDetailsViewModel(Investigation investigation, RequestBus requestBus)
        {
            Investigation = investigation ?? throw new ArgumentNullException(nameof(investigation));
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

            Investigation.DateChanged += HandleInvestigationDateChanged;
            Investigation.SentByChanged += HandleInvestigationSentByChanged;

            RetrieveMedics();
            RetrieveClinics();

            UpdateTitle();
        }

        private void RetrieveMedics()
        {
            GetAllMedicsRequest request = new GetAllMedicsRequest();
            requestBus.ProcessRequest<GetAllMedicsRequest, List<Medic>>(request)
                .ContinueWith(t => Medics = t.Result
                    .OrderBy(x => x.Name)
                    .ToList());
        }

        private void RetrieveClinics()
        {
            GetAllClinicsRequest request = new GetAllClinicsRequest();
            requestBus.ProcessRequest<GetAllClinicsRequest, List<Clinic>>(request)
                .ContinueWith(t => Clinics = t.Result
                    .OrderBy(x => x.Name)
                    .ToList());
        }

        private void HandleInvestigationDateChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void HandleInvestigationSentByChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            Title = string.Format("{0:yyyy MM dd} - Sent by {1} - (investigation)", Investigation.Date, Investigation.SentBy?.Name);
        }
    }
}