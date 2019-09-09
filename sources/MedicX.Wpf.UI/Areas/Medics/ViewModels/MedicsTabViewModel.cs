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
using System.Windows.Threading;
using DustInTheWind.MedicX.Application.SetCurrentItem;
using DustInTheWind.MedicX.Domain.Collections;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using MedicX.Wpf.UI.Areas.Main.Commands;
using MedicX.Wpf.UI.Areas.Medics.Commands;

namespace MedicX.Wpf.UI.Areas.Medics.ViewModels
{
    internal class MedicsTabViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private readonly MedicXProject medicXProject;
        private MedicItemViewModel selectedMedic;
        private readonly CollectionViewSource medicsSource;
        private string searchText;
        private readonly Dispatcher dispatcher;

        public ICollectionView Medics { get; }

        public MedicItemViewModel SelectedMedic
        {
            get => selectedMedic;
            set
            {
                if (selectedMedic == value)
                    return;

                selectedMedic = value;
                OnPropertyChanged();

                SetCurrentItem(selectedMedic?.Medic);
            }
        }

        private void SetCurrentItem(Medic medic)
        {
            SetCurrentItemRequest request = new SetCurrentItemRequest
            {
                NewCurrentItem = medic
            };

            AsyncUtil.RunSync(() => requestBus.ProcessRequest(request));
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
                    if (Medics.Filter != null)
                        Medics.Filter = null;
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

        public MedicsTabViewModel(RequestBus requestBus, MedicXProject medicXProject)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            AddMedicCommand = new AddMedicCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            medicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<MedicItemViewModel>(medicXProject.Medics
                    .Select(x => new MedicItemViewModel(x))
                    .ToList())
            };
            Medics = medicsSource.View;
            medicXProject.Medics.Added += HandleMedicAdded;

            medicXProject.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleMedicAdded(object sender, MedicAddedEventArgs e)
        {
            if (medicsSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
                MedicItemViewModel medicItemViewModel = new MedicItemViewModel(e.Medic);
                medics.Add(medicItemViewModel);
            }
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            dispatcher.InvokeAsync(() =>
            {
                Medic medic = medicXProject.CurrentItem as Medic;

                if (medic == null)
                    return;

                IEnumerable<MedicItemViewModel> medicsViewModels = medicsSource.Source as IEnumerable<MedicItemViewModel>;

                if (medicsViewModels == null)
                    return;

                SelectedMedic = medicsViewModels.FirstOrDefault(x => x.Medic == medic);
            });
        }

        private bool FilterMedic(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            MedicItemViewModel medicItemViewModel = o as MedicItemViewModel;

            return medicItemViewModel?.Medic?.Contains(searchText) ?? false;
        }
    }
}