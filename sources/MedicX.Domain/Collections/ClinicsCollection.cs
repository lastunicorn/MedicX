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

namespace DustInTheWind.MedicX.Domain.Collections
{
    public class ClinicsCollection : ICollection<Clinic>
    {
        private readonly List<Clinic> clinics = new List<Clinic>();

        public int Count => clinics.Count;

        public bool IsReadOnly => false;

        public event EventHandler Changed;
        public event EventHandler<ClinicAddedEventArgs> Added;

        public Clinic AddNew()
        {
            Clinic clinic = new Clinic
            {
                Id = Guid.NewGuid(),
                Address = new Address(),
                Phones = new ObservableCollection<string>()
            };

            AddInternal(clinic);

            return clinic;
        }

        public void Add(Clinic clinic)
        {
            if (clinic == null) throw new ArgumentNullException(nameof(clinic));

            AddInternal(clinic);
        }

        private void AddInternal(Clinic clinic)
        {
            clinics.Add(clinic);

            clinic.Changed += HandleClinicChanged;

            OnChanged();
            OnAdded(new ClinicAddedEventArgs(clinic));
        }

        public void Clear()
        {
            foreach (Clinic clinic in clinics)
                clinic.Changed -= HandleClinicChanged;

            clinics.Clear();

            OnChanged();
        }

        public bool Contains(Clinic clinic)
        {
            return clinics.Contains(clinic);
        }

        public void CopyTo(Clinic[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex is less than 0.");

            if (clinics.Count > array.Length)
                throw new ArgumentException("The number of elements in the collection is greater than the available space of the destination array.", nameof(array));

            if (clinics.Count > array.Length - arrayIndex)
                throw new ArgumentException("The number of elements in the collection is greater than the available space from arrayIndex to the end of the destination array.", nameof(arrayIndex));

            clinics.CopyTo(array, arrayIndex);
        }

        public bool Remove(Clinic clinic)
        {
            if (clinic == null) throw new ArgumentNullException(nameof(clinic));

            clinic.Changed -= HandleClinicChanged;

            bool success = clinics.Remove(clinic);

            OnChanged();

            return success;
        }

        public IEnumerator<Clinic> GetEnumerator()
        {
            return clinics.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void HandleClinicChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAdded(ClinicAddedEventArgs e)
        {
            Added?.Invoke(this, e);
        }
    }
}