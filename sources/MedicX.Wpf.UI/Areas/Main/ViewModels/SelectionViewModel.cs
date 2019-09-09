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
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using MedicX.Wpf.UI.Areas.Clinics.ViewModels;
using MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels;
using MedicX.Wpf.UI.Areas.Medics.ViewModels;

namespace MedicX.Wpf.UI.Areas.Main.ViewModels
{
    public class SelectionViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private readonly MedicXProject medicXProject;

        private TabItemViewModel selectedTab;

        public List<TabItemViewModel> Tabs { get; }

        public TabItemViewModel SelectedTab
        {
            get => selectedTab;
            set
            {
                if (selectedTab == value)
                    return;

                selectedTab = value;
                OnPropertyChanged();

                UpdateCurrentItem();
            }
        }

        public SelectionViewModel(RequestBus requestBus, MedicXProject medicXProject)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            Tabs = CreateTabs();
            SelectedTab = Tabs[2];
        }

        private List<TabItemViewModel> CreateTabs()
        {
            return new List<TabItemViewModel>
            {
                new TabItemViewModel
                {
                    Header = "Medics",
                    Content = new MedicsTabViewModel(requestBus, medicXProject)
                },
                new TabItemViewModel
                {
                    Header = "Clinics",
                    Content = new ClinicsTabViewModel(requestBus, medicXProject)
                },
                new TabItemViewModel
                {
                    Header = "Medical Events",
                    Content = new MedicalEventsTabViewModel(requestBus, medicXProject)
                }
            };
        }

        private void UpdateCurrentItem()
        {
            switch (selectedTab?.Content)
            {
                case MedicsTabViewModel medicsViewModel:
                    medicXProject.CurrentItem = medicsViewModel.SelectedMedic?.Medic;
                    break;

                case ClinicsTabViewModel clinicsViewModel:
                    medicXProject.CurrentItem = clinicsViewModel.SelectedClinic?.Clinic;
                    break;

                case MedicalEventsTabViewModel medicalEventsViewModel:
                    switch (medicalEventsViewModel.SelectedMedicalEvent)
                    {
                        case ConsultationItemViewModel consultation:
                            medicXProject.CurrentItem = consultation.Value;
                            break;

                        case InvestigationItemViewModel investigation:
                            medicXProject.CurrentItem = investigation.Value;
                            break;

                        default:
                            medicXProject.CurrentItem = null;
                            break;
                    }

                    break;

                default:
                    medicXProject.CurrentItem = null;
                    break;
            }
        }
    }
}