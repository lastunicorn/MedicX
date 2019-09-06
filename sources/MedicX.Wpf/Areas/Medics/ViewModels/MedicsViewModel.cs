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

namespace DustInTheWind.MedicX.Wpf.Areas.Medics.ViewModels
{
    internal class MedicsViewModel : ViewModelBase
    {
        private readonly MedicXProject medicXProject;
        private MedicListItemViewModel selectedMedic;
        private readonly CollectionViewSource medicsSource;
        private string searchText;

        public ICollectionView Medics { get; }

        public MedicListItemViewModel SelectedMedic
        {
            get => selectedMedic;
            set
            {
                if (selectedMedic == value)
                    return;

                selectedMedic = value;
                OnPropertyChanged();

                medicXProject.CurrentItem = selectedMedic?.Medic;
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
                    if (Medics.Filter != null) Medics.Filter = null;
                }
                else
                {
                    if (Medics.Filter == null)
                        Medics.Filter = FilterMedic;
                    else
                        Medics.Refresh();
                }
            }
        }

        public AddMedicCommand AddMedicCommand { get; }
        public RelayCommand ClearSearchTextCommand { get; }

        public MedicsViewModel(MedicXProject medicXProject)
        {
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            AddMedicCommand = new AddMedicCommand(medicXProject);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            medicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<MedicListItemViewModel>(medicXProject.Medics
                    .Select(x => new MedicListItemViewModel(x))
                    .ToList())
            };
            Medics = medicsSource.View;
            medicXProject.Medics.Added += HandleMedicAdded;

            medicXProject.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleMedicAdded(object sender, MedicAddedEventArgs e)
        {
            if (medicsSource.Source is ObservableCollection<MedicListItemViewModel> medics)
            {
                MedicListItemViewModel medicListItemViewModel = new MedicListItemViewModel(e.Medic);
                medics.Add(medicListItemViewModel);
            }
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            Medic medic = medicXProject.CurrentItem as Medic;

            if (medic == null)
                return;

            IEnumerable<MedicListItemViewModel> medicsViewModels = medicsSource.Source as IEnumerable<MedicListItemViewModel>;

            if (medicsViewModels == null)
                return;

            SelectedMedic = medicsViewModels.FirstOrDefault(x => x.Medic == medic);
        }

        private bool FilterMedic(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            MedicListItemViewModel medicListItemViewModel = o as MedicListItemViewModel;

            return medicListItemViewModel?.Medic?.Contains(searchText) ?? false;
        }
    }
}