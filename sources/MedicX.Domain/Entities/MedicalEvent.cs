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
using System.Linq;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class MedicalEvent : Record, IEquatable<MedicalEvent>
    {
        private Medic medic;
        private Clinic clinic;

        public Guid Id { get; set; }

        public Medic Medic
        {
            get => medic;
            set
            {
                medic = value;
                OnMedicChanged();
                OnChanged();
            }
        }

        public Clinic Clinic
        {
            get => clinic;
            set
            {
                clinic = value;
                OnChanged();
            }
        }

        public event EventHandler MedicChanged;

        public override void CopyFrom(MedicalEvent medicalEvent)
        {
            base.CopyFrom(medicalEvent);

            Id = medicalEvent.Id;
            Medic = medicalEvent.Medic;
            Clinic = medicalEvent.Clinic;
        }

        public virtual bool Contains(string text)
        {
            return (Clinic != null && Clinic.Contains(text)) ||
                   (Date.ToString("yyyy MM dd").IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Medic != null && Medic.Contains(text)) ||
                   (Labels != null && Labels.Any(x => x != null && x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                   (Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public bool Equals(MedicalEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Equals(medic, other.medic) &&
                   Equals(clinic, other.clinic) &&
                   Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MedicalEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (medic != null ? medic.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (clinic != null ? clinic.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }

        protected virtual void OnMedicChanged()
        {
            MedicChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}