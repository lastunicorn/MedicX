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
using DustInTheWind.MedicX.Application.SetMedicAsCurrent;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.UI.Areas.Main.Commands;
using DustInTheWind.MedicX.Wpf.UI.Areas.Medics.Commands;
using EventBusModel;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.Medics.ViewModels
{
    internal class MedicsTabViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private MedicItemViewModel selectedMedic;
        private readonly CollectionViewSource medicsViewSource;
        private string searchText;
        private readonly Dispatcher dispatcher;
        private bool isMedicsListEnabled;

        public ICollectionView Medics => medicsViewSource.View;

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

        private void SetCurrentItem(MedicDto medic)
        {
            IsMedicsListEnabled = false;

            SetMedicAsCurrentRequest request = new SetMedicAsCurrentRequest
            {
                NewCurrentItem = medic
            };

            requestBus.ProcessRequest(request)
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                        SelectedMedic = null;

                    IsMedicsListEnabled = true;
                }, TaskContinuationOptions.ExecuteSynchronously)
                .ConfigureAwait(true);
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

        public bool IsMedicsListEnabled
        {
            get => isMedicsListEnabled;
            set
            {
                isMedicsListEnabled = value;
                OnPropertyChanged();
            }
        }

        public MedicsTabViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            AddMedicCommand = new AddMedicCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            medicsViewSource = new CollectionViewSource
            {
                Source = new ObservableCollection<MedicItemViewModel>()
            };
            Medics.SortDescriptions.Add(new SortDescription(nameof(MedicItemViewModel.Name), ListSortDirection.Ascending));

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
            eventAggregator["NewMedicAdded"].Subscribe(new Action<MedicDto>(HandleNewMedicAdded));
            eventAggregator["MedicNameChanged"].Subscribe(new Action<MedicDto>(HandleMedicNameChanged));
            eventAggregator["MedicSpecializationsChanged"].Subscribe(new Action<MedicDto>(HandleMedicSpecializationsChanged));

            UpdateListOfMedics();
        }

        private void UpdateListOfMedics()
        {
            IsMedicsListEnabled = false;

            GetAllMedicsRequest request = new GetAllMedicsRequest();
            requestBus.ProcessRequest<GetAllMedicsRequest, List<MedicDto>>(request)
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                    {
                        Medics.DeferRefresh();
                        try
                        {
                            if (medicsViewSource.Source is ObservableCollection<MedicItemViewModel> medics)
                            {
                                medics.Clear();

                                IEnumerable<MedicItemViewModel> viewModels = t.Result.Select(x => new MedicItemViewModel(x));

                                foreach (MedicItemViewModel viewModel in viewModels)
                                    medics.Add(viewModel);
                            }
                        }
                        finally
                        {
                            Medics.Refresh();
                        }

                        IsMedicsListEnabled = true;
                    }
                    else
                    {
                        // todo: display an error message
                    }
                }, TaskContinuationOptions.ExecuteSynchronously)
                .ConfigureAwait(true);
        }

        private void HandleMedicNameChanged(MedicDto medic)
        {
            if (medicsViewSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
                MedicItemViewModel viewModel = medics.FirstOrDefault(x => x.Medic.Id == medic.Id);

                if (viewModel != null)
                {
                    viewModel.UpdateFrom(medic);
                    Medics.Refresh();
                }
            }
        }

        private void HandleMedicSpecializationsChanged(MedicDto medic)
        {
            if (medicsViewSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
                MedicItemViewModel viewModel = medics.FirstOrDefault(x => x.Medic.Id == medic.Id);

                if (viewModel != null)
                {
                    viewModel.UpdateFrom(medic);
                    Medics.Refresh();
                }
            }
        }

        private void HandleNewMedicAdded(MedicDto newMedic)
        {
            if (medicsViewSource.Source is ObservableCollection<MedicItemViewModel> medics)
            {
                MedicItemViewModel medicItemViewModel = new MedicItemViewModel(newMedic);
                medics.Add(medicItemViewModel);
            }
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            dispatcher.InvokeAsync(() =>
            {
                if (currentItem is MedicDto medic)
                {
                    if (medicsViewSource.Source is ObservableCollection<MedicItemViewModel> medics)
                        SelectedMedic = medics.FirstOrDefault(x => x.Medic.Id == medic.Id);
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