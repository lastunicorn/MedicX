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

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class SelectionViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;

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

        public SelectionViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

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
                    Content = new MedicsViewModel(applicationState)
                },
                new TabItemViewModel
                {
                    Header = "Clinics",
                    Content = new ClinicsViewModel(applicationState)
                },
                new TabItemViewModel
                {
                    Header = "Medical Events",
                    Content = new MedicalEventsViewModel(applicationState)
                }
            };
        }

        private void UpdateCurrentItem()
        {
            switch (selectedTab?.Content)
            {
                case MedicsViewModel medicsViewModel:
                    applicationState.CurrentItem = medicsViewModel.SelectedMedic?.Value;
                    break;

                case ClinicsViewModel clinicsViewModel:
                    applicationState.CurrentItem = clinicsViewModel.SelectedClinic?.Value;
                    break;

                case MedicalEventsViewModel medicalEventsViewModel:
                    applicationState.CurrentItem = medicalEventsViewModel.SelectedMedicalEvent;
                    break;

                default:
                    applicationState.CurrentItem = null;
                    break;
            }
        }
    }
}