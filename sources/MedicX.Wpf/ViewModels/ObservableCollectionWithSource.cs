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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class ObservableCollectionWithSource<T, TSource> : ObservableCollection<T>
    {
        private ObservableCollection<TSource> sourceCollection;

        public Func<TSource, T> FromSourceConverter { get; set; }

        public ObservableCollection<TSource> SourceCollection
        {
            get => sourceCollection;
            set
            {
                if (sourceCollection != null)
                    sourceCollection.CollectionChanged -= HandleSourceCollectionChanged;

                sourceCollection = value;

                ClearItems();

                if (sourceCollection != null)
                {
                    AddItems(sourceCollection, 0);

                    sourceCollection.CollectionChanged += HandleSourceCollectionChanged;
                }
            }
        }

        private void HandleSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e.NewItems, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e.OldStartingIndex, e.OldItems.Count);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    ReplaceItems(e.NewItems, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Move:
                    Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddItems(IEnumerable items, int index)
        {
            IEnumerable<T> newItems = items
                .Cast<TSource>()
                .Select(x => FromSourceConverter(x));

            int i = index;

            foreach (T item in newItems)
            {
                Insert(i, item);
                i++;
            }
        }

        private void RemoveItems(int index, int count)
        {
            for (int i = 0; i < count; i++)
                RemoveAt(index);
        }

        private void ReplaceItems(IEnumerable items, int index)
        {
            IEnumerable<T> newItems = items
                .Cast<TSource>()
                .Select(x => FromSourceConverter(x));

            int i = index;

            foreach (T item in newItems)
            {
                this[i] = item;
                i++;
            }
        }
    }
}