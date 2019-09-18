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
using MedicX.Wpf.UI.Areas.Clinics.Commands;
using MedicX.Wpf.UI.Areas.Main.Commands;

namespace MedicX.Wpf.UI.Areas.Clinics.ViewModels
{
    internal class ClinicsTabViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private ClinicItemViewModel selectedClinic;
        private readonly CollectionViewSource clinicsSource;
        private string searchText;
        private readonly Dispatcher dispatcher;

        public ICollectionView Clinics { get; }

        public ClinicItemViewModel SelectedClinic
        {
            get => selectedClinic;
            set
            {
                if (selectedClinic == value)
                    return;

                selectedClinic = value;
                OnPropertyChanged();

                SetCurrentItem(selectedClinic?.Clinic);
            }
        }

        private void SetCurrentItem(Clinic clinic)
        {
            SetCurrentItemRequest request = new SetCurrentItemRequest
            {
                NewCurrentItem = clinic
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

        public ClinicsTabViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

            AddClinicCommand = new AddClinicCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            //GetAllClinicsRequest request = new GetAllClinicsRequest();
            //List<Clinic> clinics = requestBus.ProcessRequest<GetAllClinicsRequest, List<Clinic>>(request).Result;

            clinicsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<ClinicItemViewModel>()
                //Source = new ObservableCollection<ClinicItemViewModel>(medicXProject.Clinics
                //    .Select(x =>
                //    {
                //        x.NameChanged += HandleClinicNameChanged;
                //        return new ClinicItemViewModel(x);
                //    })
                //    .ToList())
            };
            Clinics = clinicsSource.View;
            Clinics.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
            eventAggregator["NewClinicAdded"].Subscribe(new Action<Clinic>(HandleNewClinicAdded));
        }

        private void HandleClinicNameChanged(object sender, EventArgs e)
        {
            dispatcher.InvokeAsync(() => Clinics.Refresh());
        }

        private void HandleNewClinicAdded(Clinic newClinic)
        {
            if (clinicsSource.Source is ObservableCollection<ClinicItemViewModel> clinics)
            {
                newClinic.NameChanged += HandleClinicNameChanged;
                ClinicItemViewModel clinicToBeAdded = new ClinicItemViewModel(newClinic);
                clinics.Add(clinicToBeAdded);
            }
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            dispatcher.InvokeAsync(() =>
            {
                if (currentItem is Clinic clinic)
                    SelectedClinic = GetNextSelectedClinic(clinic);
            });
        }

        private ClinicItemViewModel GetNextSelectedClinic(Clinic clinic)
        {
            if (clinic == null)
                return null;

            IEnumerable<ClinicItemViewModel> clinicsViewModels = clinicsSource.Source as IEnumerable<ClinicItemViewModel>;
            return clinicsViewModels?.FirstOrDefault(x => x.Clinic == clinic);
        }

        private bool FilterClinic(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            ClinicItemViewModel clinicItemViewModel = o as ClinicItemViewModel;

            return clinicItemViewModel?.Clinic?.Contains(searchText) ?? false;
        }
    }
}