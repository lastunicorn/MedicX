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
using System.Collections.ObjectModel;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class MedicalEventsViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
        private MedicalEvent selectedMedicalEvent;

        public ObservableCollection<MedicalEvent> MedicalEvents { get; }

        public MedicalEvent SelectedMedicalEvent
        {
            get => selectedMedicalEvent;
            set
            {
                if (selectedMedicalEvent == value)
                    return;

                selectedMedicalEvent = value;
                OnPropertyChanged();

                applicationState.CurrentItem = selectedMedicalEvent;
            }
        }

        public MedicalEventsViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            MedicalEvents = applicationState.MedicalEvents;
        }
    }
}