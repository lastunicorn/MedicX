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
using System.Threading.Tasks;
using DustInTheWind.MedicX.Application.GetAllMedics;
using DustInTheWind.MedicX.Application.GetCurrentItem;
using DustInTheWind.MedicX.Application.SetCurrentItem;
using DustInTheWind.MedicX.RequestBusModel;
using EventBusModel;
using MedicX.Wpf.UI.Areas.Clinics.ViewModels;
using MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels;
using MedicX.Wpf.UI.Areas.Medics.ViewModels;

namespace MedicX.Wpf.UI.Areas.Main.ViewModels
{
    public class TabSelectionViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private readonly EventAggregator eventAggregator;

        private TabItemViewModel selectedTab;
        private bool areTabsEnabled;

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

        public bool AreTabsEnabled
        {
            get => areTabsEnabled;
            set
            {
                areTabsEnabled = value;
                OnPropertyChanged();
            }
        }

        public TabSelectionViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            Tabs = CreateTabs();
            UpdateSelectedTab();

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
        }

        private void HandleCurrentItemChanged(object newCurrentItem)
        {
            switch (newCurrentItem)
            {
                case Medic _:
                    SelectedTab = Tabs[0];
                    break;
            }
        }

        private void UpdateSelectedTab()
        {
            AreTabsEnabled = false;

            GetCurrentItemRequest request = new GetCurrentItemRequest();

            requestBus.ProcessRequest<GetCurrentItemRequest, object>(request)
                .ContinueWith(t =>
                {
                    switch (t.Result)
                    {
                        case Medic _:
                            SelectedTab = Tabs[0];
                            break;

                        default:
                            SelectedTab = Tabs[0];
                            break;
                    }

                    AreTabsEnabled = true;
                }, TaskContinuationOptions.ExecuteSynchronously)
                .ConfigureAwait(true);
        }

        private List<TabItemViewModel> CreateTabs()
        {
            return new List<TabItemViewModel>
            {
                new TabItemViewModel
                {
                    Header = "Medics",
                    Content = new MedicsTabViewModel(requestBus, eventAggregator)
                },
                new TabItemViewModel
                {
                    Header = "Clinics",
                    Content = new ClinicsTabViewModel(requestBus, eventAggregator)
                },
                //new TabItemViewModel
                //{
                //    Header = "Medical Events",
                //    Content = new MedicalEventsTabViewModel(requestBus, eventAggregator, medicXProject)
                //}
            };
        }

        private void UpdateCurrentItem()
        {
            SetCurrentItemRequest request = new SetCurrentItemRequest
            {
                NewCurrentItem = CalculateNewCurrentItem()
            };

            requestBus.ProcessRequest(request)
                .ConfigureAwait(false);
        }

        private object CalculateNewCurrentItem()
        {
            switch (selectedTab?.Content)
            {
                case MedicsTabViewModel medicsViewModel:
                    return medicsViewModel.SelectedMedic?.Medic;

                case ClinicsTabViewModel clinicsViewModel:
                    return clinicsViewModel.SelectedClinic?.Clinic;

                case MedicalEventsTabViewModel medicalEventsViewModel:
                    {
                        switch (medicalEventsViewModel.SelectedMedicalEvent)
                        {
                            case ConsultationItemViewModel consultation:
                                return consultation.Value;

                            case InvestigationItemViewModel investigation:
                                return investigation.Value;

                            default:
                                return null;
                        }
                    }

                default:
                    return null;
            }
        }
    }
}