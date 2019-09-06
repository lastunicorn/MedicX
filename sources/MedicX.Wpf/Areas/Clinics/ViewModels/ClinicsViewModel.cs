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
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DustInTheWind.MedicX.Common;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Wpf.Commands;

namespace DustInTheWind.MedicX.Wpf.Areas.Clinics.ViewModels
{
    internal class ClinicsViewModel : ViewModelBase
    {
        private readonly MedicXProject medicXProject;
        private ClinicListItemViewModel selectedClinic;
        private readonly CollectionViewSource clinicsSource;
        private string searchText;

        public ICollectionView Clinics { get; }

        public ClinicListItemViewModel SelectedClinic
        {
            get => selectedClinic;
            set
            {
                if (selectedClinic == value)
                    return;

                selectedClinic = value;
                OnPropertyChanged();

                medicXProject.CurrentItem = selectedClinic?.Clinic;
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();

                if (searchText == null)
                {
                    if (Clinics.Filter != null) Clinics.Filter = null;
                }
                else
                {
                    if (Clinics.Filter == null)
                        Clinics.Filter = FilterClinic;
                    else
                        Clinics.Refresh();
                }
            }
        }

        public AddClinicCommand AddClinicCommand { get; }
        public RelayCommand ClearSearchTextCommand { get; }

        public ClinicsViewModel(MedicXProject medicXProject)
        {
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            AddClinicCommand = new AddClinicCommand(medicXProject);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            clinicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<ClinicListItemViewModel>(medicXProject.Clinics
                    .Select(x => new ClinicListItemViewModel(x))
                    .ToList())
            };
            Clinics = clinicsSource.View;
            medicXProject.Clinics.Added += HandleClinicAdded;

            medicXProject.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleClinicAdded(object sender, ClinicAddedEventArgs e)
        {
            if (clinicsSource.Source is ObservableCollection<ClinicListItemViewModel> clinics)
            {
                ClinicListItemViewModel clinicToBeAdded = new ClinicListItemViewModel(e.Clinic);
                clinics.Add(clinicToBeAdded);
            }
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            Clinic clinic = medicXProject.CurrentItem as Clinic;

            if (clinic == null)
                return;

            IEnumerable<ClinicListItemViewModel> clinicsViewModels = clinicsSource.Source as IEnumerable<ClinicListItemViewModel>;

            if (clinicsViewModels == null)
                return;

            SelectedClinic = clinicsViewModels.FirstOrDefault(x => x.Clinic == clinic);
        }

        private bool FilterClinic(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            ClinicListItemViewModel clinicListItemViewModel = o as ClinicListItemViewModel;

            return clinicListItemViewModel?.Clinic?.Contains(searchText) ?? false;
        }
    }
}