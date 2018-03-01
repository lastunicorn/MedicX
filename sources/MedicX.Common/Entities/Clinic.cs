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

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Clinic : IEquatable<Clinic>
    {
        private string name;
        private Address address;
        private ObservableCollection<string> phones;
        private string program;
        private string comments;

        public Guid Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;

                OnNameChanged();
                OnChanged();
            }
        }

        public Address Address
        {
            get => address;
            set
            {
                if (address != null)
                    address.Changed -= HandleAddressChanged;

                address = value;

                if (address != null)
                    address.Changed += HandleAddressChanged;

                OnChanged();
            }
        }

        public ObservableCollection<string> Phones
        {
            get => phones;
            set
            {
                if (phones != null)
                    phones.CollectionChanged -= HandlePhonesCollectionChanged;

                phones = value;

                if (phones != null)
                    phones.CollectionChanged += HandlePhonesCollectionChanged;

                OnChanged();
            }
        }

        public string Program
        {
            get => program;
            set
            {
                program = value;
                OnChanged();
            }
        }

        public string Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnChanged();
            }
        }

        public event EventHandler NameChanged;
        public event EventHandler Changed;

        private void HandleAddressChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void HandlePhonesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }

        public void CopyFrom(Clinic clinic)
        {
            Id = clinic.Id;
            Name = clinic.Name;
            Address = clinic.Address;
            Phones = new ObservableCollection<string>(clinic.Phones);
            Program = clinic.Program;
            Comments = clinic.Comments;
        }

        public bool Contains(string text)
        {
            return (Name != null && Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Address != null && Address.Contains(text)) ||
                   (Phones != null && Phones.Any(x => x != null && x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                   (Program != null && Program.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public bool Equals(Clinic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name) &&
                   Equals(address, other.address) &&
                   ((phones == null && other.phones == null) || (phones != null && other.phones != null && phones.SequenceEqual(other.phones))) &&
                   string.Equals(program, other.program) &&
                   string.Equals(comments, other.comments) &&
                   Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Clinic)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (address != null ? address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (phones != null ? phones.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (program != null ? program.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (comments != null ? comments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }
    }
}