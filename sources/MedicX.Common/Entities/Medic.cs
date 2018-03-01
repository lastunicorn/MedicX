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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Medic : Person, IEquatable<Medic>
    {
        private ObservableCollection<string> specializations;

        public ObservableCollection<string> Specializations
        {
            get => specializations;
            set
            {
                if (specializations != null)
                    specializations.CollectionChanged -= HandleSpecializationsCollectionChanged;

                specializations = value;

                if (specializations != null)
                    specializations.CollectionChanged += HandleSpecializationsCollectionChanged;

                OnChanged();
            }
        }

        private void HandleSpecializationsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }

        public void CopyFrom(Medic medic)
        {
            base.CopyFrom(medic);
            Specializations = new ObservableCollection<string>(medic.Specializations);
        }

        public override void CopyFrom(Person person)
        {
            if (person is Medic medic)
                CopyFrom(medic);
            else
                base.CopyFrom(person);
        }

        public override bool Contains(string text)
        {
            return base.Contains(text) ||
                   Specializations != null && Specializations.Any(x => x != null && x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public bool Equals(Medic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return base.Equals(other) &&
                   (
                       (specializations == null && other.specializations == null) ||
                       (specializations != null && other.specializations != null && specializations.SequenceEqual(other.specializations))
                   );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((Medic)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (specializations != null ? specializations.GetHashCode() : 0);
            }
        }
    }
}