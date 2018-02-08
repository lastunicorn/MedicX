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
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class EventsViewModel : ObservableCollection<EventViewModel>
    {
        private ObservableCollection<MedicalEvent> sourceEvents;

        public ObservableCollection<MedicalEvent> SourceEvents
        {
            get => sourceEvents;
            set
            {
                if (sourceEvents != null)
                    sourceEvents.CollectionChanged -= HandleEventsCollectionChanged;

                sourceEvents = value;

                ClearItems();

                if (sourceEvents != null)
                {
                    List<EventViewModel> newEventViewModels = sourceEvents
                        .Select(x => x.ToViewModel())
                        .ToList();

                    foreach (EventViewModel newEventViewModel in newEventViewModels)
                        InsertItem(Count, newEventViewModel);

                    sourceEvents.CollectionChanged += HandleEventsCollectionChanged;
                }
            }
        }

        private void HandleEventsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        IEnumerable<EventViewModel> newEvents = e.NewItems
                            .Cast<MedicalEvent>()
                            .Select(x => x.ToViewModel());

                        int index = e.NewStartingIndex;

                        foreach (EventViewModel eventViewModel in newEvents)
                        {
                            Insert(index, eventViewModel);
                            index++;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        int count = e.OldItems.Count;

                        for (int i = 0; i < count; i++)
                            RemoveAt(e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        IEnumerable<EventViewModel> newEvents = e.NewItems
                            .Cast<MedicalEvent>()
                            .Select(x => x.ToViewModel());

                        int index = e.NewStartingIndex;

                        foreach (EventViewModel eventViewModel in newEvents)
                        {
                            this[index] = eventViewModel;
                            index++;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}