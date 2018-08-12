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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Business
{
    public class MedicalEventCollection : ICollection<MedicalEvent>
    {
        private readonly List<MedicalEvent> medicalEvents = new List<MedicalEvent>();

        public int Count => medicalEvents.Count;

        public bool IsReadOnly => false;

        public event EventHandler Changed;
        public event EventHandler<MedicalEventAddedEventArgs> Added;

        public void Add(MedicalEvent medicalEvent)
        {
            if (medicalEvent == null) throw new ArgumentNullException(nameof(medicalEvent));

            medicalEvents.Add(medicalEvent);

            medicalEvent.Changed += HandleMedicChanged;

            OnChanged();
            OnAdded(new MedicalEventAddedEventArgs(medicalEvent));
        }

        public void Clear()
        {
            foreach (MedicalEvent medicalEvent in medicalEvents)
                medicalEvent.Changed -= HandleMedicChanged;

            medicalEvents.Clear();

            OnChanged();
        }

        public bool Contains(MedicalEvent medicalEvent)
        {
            return medicalEvents.Contains(medicalEvent);
        }

        public void CopyTo(MedicalEvent[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex is less than 0.");

            if (medicalEvents.Count > array.Length)
                throw new ArgumentException("The number of elements in the collection is greater than the available space of the destination array.", nameof(array));

            if (medicalEvents.Count > array.Length - arrayIndex)
                throw new ArgumentException("The number of elements in the collection is greater than the available space from arrayIndex to the end of the destination array.", nameof(arrayIndex));

            medicalEvents.CopyTo(array, arrayIndex);
        }

        public bool Remove(MedicalEvent medicalEvent)
        {
            if (medicalEvent == null) throw new ArgumentNullException(nameof(medicalEvent));

            medicalEvent.Changed -= HandleMedicChanged;

            bool success = medicalEvents.Remove(medicalEvent);

            OnChanged();

            return success;
        }

        public IEnumerator<MedicalEvent> GetEnumerator()
        {
            return medicalEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void HandleMedicChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAdded(MedicalEventAddedEventArgs e)
        {
            Added?.Invoke(this, e);
        }
    }
}