using System;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class Treatment : MedicalEvent, IEquatable<Treatment>
    {
        private string description;

        public string Description
        {
            get => description;
            set => description = value;
        }

        public bool Equals(Treatment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && description == other.description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Treatment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (description != null ? description.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}