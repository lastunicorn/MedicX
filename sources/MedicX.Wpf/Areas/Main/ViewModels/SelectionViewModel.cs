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
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.Areas.Clinics.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.MedicalEvents.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.Medics.ViewModels;

namespace DustInTheWind.MedicX.Wpf.Areas.Main.ViewModels
{
    internal class SelectionViewModel : ViewModelBase
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
                    Content = new MedicsViewModel(medicXProject)
                },
                new TabItemViewModel
                {
                    Header = "Clinics",
                    Content = new ClinicsViewModel(requestBus, medicXProject)
                },
                new TabItemViewModel
                {
                    Header = "Medical Events",
                    Content = new MedicalEventsViewModel(medicXProject)
                }
            };
        }

        private void UpdateCurrentItem()
        {
            switch (selectedTab?.Content)
            {
                case MedicsViewModel medicsViewModel:
                    medicXProject.CurrentItem = medicsViewModel.SelectedMedic?.Medic;
                    break;

                case ClinicsViewModel clinicsViewModel:
                    medicXProject.CurrentItem = clinicsViewModel.SelectedClinic?.Clinic;
                    break;

                case MedicalEventsViewModel medicalEventsViewModel:
                    switch (medicalEventsViewModel.SelectedMedicalEvent)
                    {
                        case ConsultationListItemViewModel consultation:
                            medicXProject.CurrentItem = consultation.Value;
                            break;

                        case InvestigationListItemViewModel investigation:
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