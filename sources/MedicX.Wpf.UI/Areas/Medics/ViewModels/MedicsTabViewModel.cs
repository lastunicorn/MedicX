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
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using DustInTheWind.MedicX.Application.GetAllMedics;
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

        public ICollectionView Medics => medicsSource.View;

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

        public MedicsTabViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            AddMedicCommand = new AddMedicCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            medicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<MedicItemViewModel>()
            };
            Medics.SortDescriptions.Add(new SortDescription(nameof(MedicItemViewModel.Name), ListSortDirection.Ascending));

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
            eventAggregator["NewMedicAdded"].Subscribe(new Action<Medic>(HandleNewMedicAdded));
            eventAggregator["MedicNameChanged"].Subscribe(new Action<Medic>(HandleMedicNameChanged));

            requestBus.ProcessRequest<GetAllMedicsRequest, List<Medic>>(new GetAllMedicsRequest())
                .ContinueWith(t =>
                {
                    if (medicsSource.Source is ObservableCollection<MedicItemViewModel> medics)
                    {
                        foreach (Medic medic in t.Result)
                        {
                            MedicItemViewModel viewModel = new MedicItemViewModel(medic);
                            medics.Add(viewModel);
                        }
                    }
                }, TaskContinuationOptions.ExecuteSynchronously)
                .ConfigureAwait(true);
        }

        private void HandleMedicNameChanged(Medic medic)
        {
            Medics.Refresh();
        }

        private void HandleNewMedicAdded(Medic newMedic)
        {
            if (medicsSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
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
                    if (medicsSource.Source is ObservableCollection<MedicItemViewModel> medics)
                        SelectedMedic = medics.FirstOrDefault(x => x.Medic == medic);
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