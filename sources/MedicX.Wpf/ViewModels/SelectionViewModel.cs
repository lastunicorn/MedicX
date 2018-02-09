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
using DustInTheWind.MedicX.Wpf.Views;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class SelectionViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
        private ObservableCollection<Medic> medics;
        private ObservableCollection<Clinic> clinics;
        private ObservableCollection<Consultation> consultations;
        private Medic selectedMedic;
        private Clinic selectedClinic;
        private Consultation selectedConsultation;
        private Tab selectedTab;

        public ObservableCollection<Medic> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged();
            }
        }

        public Medic SelectedMedic
        {
            get => selectedMedic;
            set
            {
                if (selectedMedic == value)
                    return;

                selectedMedic = value;
                OnPropertyChanged();

                applicationState.CurrentItem = selectedMedic;
            }
        }

        public ObservableCollection<Clinic> Clinics
        {
            get { return clinics; }
            set
            {
                clinics = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<Consultation> Consultations
        {
            get { return consultations; }
            set
            {
                consultations = value;
                OnPropertyChanged();
            }
        }

        public Consultation SelectedConsultation
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
                        applicationState.CurrentItem = selectedMedic;
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

        public SelectionViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            medics = applicationState.Medics;
            clinics = applicationState.Clinics;
            consultations = applicationState.Consultations;
        }
    }
}