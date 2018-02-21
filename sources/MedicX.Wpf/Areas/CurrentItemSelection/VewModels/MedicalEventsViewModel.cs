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

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class MedicalEventsViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
        private ViewModelBase selectedMedicalEvent;
        private readonly CollectionViewSource medicalEventsSource;
        private string searchText;

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
                    case ConsultationListItemViewModel consultationListItemViewModel:
                        applicationState.CurrentItem = consultationListItemViewModel.Value;
                        break;

                    case InvestigationListItemViewModel investigationListItemViewModel:
                        applicationState.CurrentItem = investigationListItemViewModel.Value;
                        break;

                    default:
                        applicationState.CurrentItem = null;
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

        public MedicalEventsViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            medicalEventsSource = new CollectionViewSource
            {
                Source = new ObservableCollection<ViewModelBase>(applicationState.MedicalEvents
                    .Select<MedicalEvent, ViewModelBase>(x =>
                    {
                        switch (x)
                        {
                            case Consultation consultation:
                                return new ConsultationListItemViewModel(consultation);

                            case Investigation investigation:
                                return new InvestigationListItemViewModel(investigation);

                            default:
                                return null;
                        }
                    })
                    .ToList())
            };
            MedicalEvents = medicalEventsSource.View;
            applicationState.MedicalEvents.CollectionChanged += HandleMedicalEventsCollectionChanged;

            applicationState.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleMedicalEventsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (medicalEventsSource.Source is ObservableCollection<ViewModelBase> medicalEvents)
                    {
                        IEnumerable<ViewModelBase> medicalEventsToBeAdded = e.NewItems
                            .Cast<MedicalEvent>()
                            .Select<MedicalEvent, ViewModelBase>(x =>
                            {
                                switch (x)
                                {
                                    case Consultation consultation:
                                        return new ConsultationListItemViewModel(consultation);

                                    case Investigation investigation:
                                        return new InvestigationListItemViewModel(investigation);

                                    default:
                                        return null;
                                }
                            });

                        foreach (ViewModelBase medicalEventToBeAdded in medicalEventsToBeAdded)
                            medicalEvents.Add(medicalEventToBeAdded);
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
            MedicalEvent medicalEvent = applicationState.CurrentItem as MedicalEvent;

            if (medicalEvent == null)
                return;

            IEnumerable<ViewModelBase> clinicsViewModels = medicalEventsSource.Source as IEnumerable<ViewModelBase>;

            if (clinicsViewModels == null)
                return;

            SelectedMedicalEvent = clinicsViewModels.FirstOrDefault(x =>
            {
                switch (x)
                {
                    case ConsultationListItemViewModel consultation:
                        return consultation.Value == medicalEvent;

                    case InvestigationListItemViewModel investigation:
                        return investigation.Value == medicalEvent;

                    default:
                        return false;
                }
            });
        }

        private bool FilterMedicalEvent(object o)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            switch (o)
            {
                case ConsultationListItemViewModel consultation:
                    return consultation.Value?.Contains(searchText) ?? false;

                case InvestigationListItemViewModel investigation:
                    return investigation.Value?.Contains(searchText) ?? false;

                default:
                    return false;
            }
        }
    }
}