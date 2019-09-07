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
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Domain
{
    public class MedicsCollection : ICollection<Medic>
    {
        private readonly List<Medic> medics = new List<Medic>();

        public int Count => medics.Count;

        public bool IsReadOnly => false;

        public event EventHandler Changed;
        public event EventHandler<MedicAddedEventArgs> Added;

        public Medic AddNew()
        {
            Medic medic = new Medic
            {
                Id = Guid.NewGuid(),
                Name = new PersonName(),
                Specializations = new ObservableCollection<string>()
            };

            AddInternal(medic);

            return medic;
        }

        public void Add(Medic medic)
        {
            if (medic == null) throw new ArgumentNullException(nameof(medic));

            AddInternal(medic);
        }

        private void AddInternal(Medic medic)
        {
            medics.Add(medic);

            medic.Changed += HandleMedicChanged;

            OnChanged();
            OnAdded(new MedicAddedEventArgs(medic));
        }

        public void Clear()
        {
            foreach (Medic medic in medics)
                medic.Changed -= HandleMedicChanged;

            medics.Clear();

            OnChanged();
        }

        public bool Contains(Medic medic)
        {
            return medics.Contains(medic);
        }

        public void CopyTo(Medic[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex is less than 0.");

            if (medics.Count > array.Length)
                throw new ArgumentException("The number of elements in the collection is greater than the available space of the destination array.", nameof(array));

            if (medics.Count > array.Length - arrayIndex)
                throw new ArgumentException("The number of elements in the collection is greater than the available space from arrayIndex to the end of the destination array.", nameof(arrayIndex));

            medics.CopyTo(array, arrayIndex);
        }

        public bool Remove(Medic medic)
        {
            if (medic == null) throw new ArgumentNullException(nameof(medic));

            medic.Changed -= HandleMedicChanged;

            bool success = medics.Remove(medic);

            OnChanged();

            return success;
        }

        public IEnumerator<Medic> GetEnumerator()
        {
            return medics.GetEnumerator();
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

        protected virtual void OnAdded(MedicAddedEventArgs e)
        {
            Added?.Invoke(this, e);
        }
    }
}