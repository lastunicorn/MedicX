﻿// MedicX
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
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.UI.Areas.Main.Commands;
using DustInTheWind.MedicX.Wpf.UI.Areas.MedicalEvents.Commands;
using EventBusModel;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.MedicalEvents.ViewModels
{
    internal class MedicalEventsTabViewModel : ViewModelBase
    {
        private readonly Project project;
        private ViewModelBase selectedMedicalEvent;
        private readonly CollectionViewSource medicalEventsSource;
        private string searchText;
        private readonly Dispatcher dispatcher;

        public ICollectionView MedicalEvents { get; }

        public ViewModelBase SelectedMedicalEvent
        {
            get => selectedMedicalEvent;
            set
            {
                if (selectedMedicalEvent == value)
                    return;

                selectedMedicalEvent = value;
                OnPropertyChanged();

                switch (selectedMedicalEvent)
                {
                    case ConsultationItemViewModel consultationListItemViewModel:
                        project.CurrentItem = consultationListItemViewModel.Value;
                        break;

                    case InvestigationItemViewModel investigationListItemViewModel:
                        project.CurrentItem = investigationListItemViewModel.Value;
                        break;

                    default:
                        project.CurrentItem = null;
                        break;
                }
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
                    if (MedicalEvents.Filter != null) MedicalEvents.Filter = null;
                }
                else
                {
                    if (MedicalEvents.Filter == null)
                        MedicalEvents.Filter = FilterMedicalEvent;
                    else
                        MedicalEvents.Refresh();
                }
            }
        }

        public AddConsultationCommand AddConsultationCommand { get; }

        public AddInvestigationCommand AddInvestigationCommand { get; }

        public RelayCommand ClearSearchTextCommand { get; }

        public MedicalEventsTabViewModel(RequestBus requestBus, EventAggregator eventAggregator, Project project)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            this.project = project ?? throw new ArgumentNullException(nameof(project));

            AddConsultationCommand = new AddConsultationCommand(requestBus);
            AddInvestigationCommand = new AddInvestigationCommand(requestBus);
            ClearSearchTextCommand = new RelayCommand(() => { SearchText = string.Empty; });

            dispatcher = Dispatcher.CurrentDispatcher;

            medicalEventsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<ViewModelBase>(RetrieveAllMedicalEvents()
                    .Select<MedicalEvent, ViewModelBase>(x =>
                    {
                        switch (x)
                        {
                            case Consultation consultation:
                                consultation.DateChanged += HandleMedicalEventDateChanged;
                                return new ConsultationItemViewModel(consultation);

                            case Investigation investigation:
                                investigation.DateChanged += HandleMedicalEventDateChanged;
                                return new InvestigationItemViewModel(investigation);

                            default:
                                return null;
                        }
                    })
                    .ToList())
            };
            MedicalEvents = medicalEventsSource.View;
            MedicalEvents.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
            eventAggregator["NewMedicalEventAdded"].Subscribe(new Action<MedicalEvent>(HandleNewMedicalEventAdded));
        }

        private IEnumerable<MedicalEvent> RetrieveAllMedicalEvents()
        {
            using (IUnitOfWork unitOfWork = project.CreateUnitOfWork())
            {
                return unitOfWork.ConsultationRepository.GetAll()
                    .Cast<MedicalEvent>()
                    .Concat(unitOfWork.InvestigationRepository.GetAll())
                    .OrderBy(x => x.Date);
            }
        }

        private void HandleNewMedicalEventAdded(MedicalEvent newMedicalEvent)
        {
            if (medicalEventsSource.Source is ObservableCollection<ViewModelBase> medicalEvents)
            {
                switch (newMedicalEvent)
                {
                    case Consultation consultation:
                        ConsultationItemViewModel consultationToBeAdded = new ConsultationItemViewModel(consultation);
                        medicalEvents.Add(consultationToBeAdded);
                        consultationToBeAdded.DateChanged += HandleMedicalEventDateChanged;
                        break;

                    case Investigation investigation:
                        InvestigationItemViewModel investigationToBeAdded = new InvestigationItemViewModel(investigation);
                        medicalEvents.Add(investigationToBeAdded);
                        investigationToBeAdded.DateChanged += HandleMedicalEventDateChanged;
                        break;
                }
            }
        }

        private void HandleMedicalEventDateChanged(object sender, EventArgs e)
        {
            MedicalEvents.Refresh();
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            dispatcher.InvokeAsync(() =>
            {
                if (currentItem is MedicalEvent medicalEvent)
                {
                    if (medicalEventsSource.Source is IEnumerable<ViewModelBase> clinicsViewModels)
                    {
                        SelectedMedicalEvent = clinicsViewModels.FirstOrDefault(x =>
                        {
                            switch (x)
                            {
                                case ConsultationItemViewModel consultation:
                                    return consultation.Value == medicalEvent;

                                case InvestigationItemViewModel investigation:
                                    return investigation.Value == medicalEvent;

                                default:
                                    return false;
                            }
                        });
                    }
                }
            });
        }

        private bool FilterMedicalEvent(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            switch (o)
            {
                case ConsultationItemViewModel consultation:
                    return consultation.Value?.Contains(searchText) ?? false;

                case InvestigationItemViewModel investigation:
                    return investigation.Value?.Contains(searchText) ?? false;

                default:
                    return false;
            }
        }
    }
}