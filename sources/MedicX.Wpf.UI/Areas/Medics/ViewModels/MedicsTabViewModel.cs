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
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using EventBusModel;
using MedicX.Wpf.UI.Areas.Main.Commands;
using MedicX.Wpf.UI.Areas.Medics.Commands;

namespace MedicX.Wpf.UI.Areas.Medics.ViewModels
{
    internal class MedicsTabViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
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

        public MedicsTabViewModel(RequestBus requestBus, EventBus eventBus, MedicXProject medicXProject)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            if (medicXProject == null) throw new ArgumentNullException(nameof(medicXProject));

            AddMedicCommand = new AddMedicCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            medicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<MedicItemViewModel>(medicXProject.Medics
                    .Select(x =>
                    {
                        x.NameChanged += HandleMedicNameChanged;
                        return new MedicItemViewModel(x);
                    })
                    .ToList())
            };
            Medics = medicsSource.View;
            Medics.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            eventBus["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
            eventBus["NewMedicAdded"].Subscribe(new Action<Medic>(HandleNewMedicAdded));
        }

        private void HandleMedicNameChanged(object sender, EventArgs e)
        {
            Medics.Refresh();
        }

        private void HandleNewMedicAdded(Medic newMedic)
        {
            if (medicsSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
                newMedic.NameChanged += HandleMedicNameChanged;
                MedicItemViewModel medicItemViewModel = new MedicItemViewModel(newMedic);
                medics.Add(medicItemViewModel);
            }
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            dispatcher.InvokeAsync(() =>
            {
                if (currentItem is Medic medic)
                {
                    if (medicsSource.Source is IEnumerable<MedicItemViewModel> medicsViewModels)
                        SelectedMedic = medicsViewModels.FirstOrDefault(x => x.Medic == medic);
                }
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