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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.Commands;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class ClinicsViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
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

                applicationState.CurrentItem = selectedClinic?.Value;
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

        public ClinicsViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            clinicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<ClinicListItemViewModel>(applicationState.Clinics
                    .Select(x => new ClinicListItemViewModel(x))
                    .ToList())
            };
            Clinics = clinicsSource.View;
            applicationState.Clinics.CollectionChanged += HandleMedicsCollectionChanged;

            applicationState.CurrentItemChanged += HandleCurrentItemChanged;

            AddClinicCommand = new AddClinicCommand(applicationState);
        }

        private void HandleMedicsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (clinicsSource.Source is ObservableCollection<ClinicListItemViewModel> clinics)
                    {
                        IEnumerable<ClinicListItemViewModel> clinicsToBeAdded = e.NewItems
                            .Cast<Clinic>()
                            .Select(x => new ClinicListItemViewModel(x));

                        foreach (ClinicListItemViewModel clinicToBeAdded in clinicsToBeAdded)
                            clinics.Add(clinicToBeAdded);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            Clinic clinic = applicationState.CurrentItem as Clinic;

            if (clinic == null)
                return;

            IEnumerable<ClinicListItemViewModel> clinicsViewModels = clinicsSource.Source as IEnumerable<ClinicListItemViewModel>;

            if (clinicsViewModels == null)
                return;

            SelectedClinic = clinicsViewModels.FirstOrDefault(x => x.Value == clinic);
        }

        private bool FilterClinic(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            ClinicListItemViewModel clinicListItemViewModel = o as ClinicListItemViewModel;

            return clinicListItemViewModel?.Value?.Contains(searchText) ?? false;
        }
    }
}