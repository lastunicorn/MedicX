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
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.UI.Areas.Clinics.ViewModels;
using DustInTheWind.MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels;
using DustInTheWind.MedicX.Wpf.UI.Areas.Medics.ViewModels;
using EventBusModel;
using MedicDto = DustInTheWind.MedicX.Application.GetAllMedics.Medic;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.Main.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private readonly EventAggregator eventAggregator;
        private object item;
        private bool isNoItemPanelVisible;


        public object Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();

                IsNoItemPanelVisible = item == null;
            }
        }

        public bool IsNoItemPanelVisible
        {
            get => isNoItemPanelVisible;
            set
            {
                isNoItemPanelVisible = value;
                OnPropertyChanged();
            }
        }
        
        public DetailsViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            isNoItemPanelVisible = true;
            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            switch (currentItem)
            {
                case Consultation consultation:
                    Item = new ConsultationDetailsViewModel(consultation, requestBus);
                    break;

                case Investigation investigation:
                    Item = new InvestigationDetailsViewModel(investigation, requestBus);
                    break;

                case MedicDto medic:
                    Item = new MedicDetailsViewModel(medic);
                    break;

                case Clinic clinic:
                    Item = new ClinicDetailsViewModel(clinic, eventAggregator);
                    break;

                default:
                    Item = currentItem;
                    break;
            }
        }
    }
}