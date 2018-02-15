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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Wpf.Commands;
using DustInTheWind.MedicX.Wpf.ViewModels;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class SelectionViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
        private MedicListItemViewModel selectedMedic;
        private Clinic selectedClinic;
        private MedicalEvent selectedConsultation;
        private Tab selectedTab;

        public List<MedicListItemViewModel> Medics { get; }

        public MedicListItemViewModel SelectedMedic
        {
            get => selectedMedic;
            set
            {
                if (selectedMedic == value)
                    return;

                selectedMedic = value;
                OnPropertyChanged();

                applicationState.CurrentItem = selectedMedic?.Medic;
            }
        }

        public ObservableCollection<Clinic> Clinics { get; }

        public Clinic SelectedClinic
        {
            get => selectedClinic;
            set
            {
                if (selectedClinic == value)
                    return;

                selectedClinic = value;
                OnPropertyChanged();

                applicationState.CurrentItem = selectedClinic;
            }
        }

        public ObservableCollection<MedicalEvent> MedicalEvents { get; }

        public MedicalEvent SelectedConsultation
        {
            get => selectedConsultation;
            set
            {
                if (selectedConsultation == value)
                    return;

                selectedConsultation = value;
                OnPropertyChanged();

                applicationState.CurrentItem = selectedConsultation;
            }
        }

        public Tab SelectedTab
        {
            get => selectedTab;
            set
            {
                selectedTab = value;

                switch (selectedTab)
                {
                    case Tab.Medics:
                        applicationState.CurrentItem = selectedMedic?.Medic;
                        break;

                    case Tab.Clinics:
                        applicationState.CurrentItem = selectedClinic;
                        break;

                    case Tab.Consultations:
                        applicationState.CurrentItem = selectedConsultation;
                        break;

                    default:
                        applicationState.CurrentItem = null;
                        break;
                }
            }
        }

        public AddMedicCommand AddMedicCommand { get; }

        public SelectionViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            Medics = applicationState.Medics.Select(x => new MedicListItemViewModel(x)).ToList();
            Clinics = applicationState.Clinics;
            MedicalEvents = applicationState.MedicalEvents;

            AddMedicCommand = new AddMedicCommand(applicationState);

            applicationState.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            Medic medic = applicationState.CurrentItem as Medic;

            if (medic == null)
                return;

            SelectedMedic = Medics.FirstOrDefault(x => x.Medic == medic);
        }
    }
}