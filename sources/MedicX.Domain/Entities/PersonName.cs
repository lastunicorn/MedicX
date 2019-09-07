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
using System.Text;

namespace DustInTheWind.MedicX.Common.Entities
{
    public class PersonName : IComparable<PersonName>, IEquatable<PersonName>
    {
        private string firstName;
        private string middleName;
        private string lastName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;

                OnFirstNameChanged();
                OnChanged();
            }
        }

        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;

                OnMiddleNameChanged();
                OnChanged();
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;

                OnLastNameChanged();
                OnChanged();
            }
        }

        public event EventHandler FirstNameChanged;
        public event EventHandler MiddleNameChanged;
        public event EventHandler LastNameChanged;
        public event EventHandler Changed;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(FirstName))
                sb.Append(FirstName);

            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(MiddleName);
            }

            if (!string.IsNullOrWhiteSpace(LastName))
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(LastName);
            }

            return sb.ToString();
        }

        public bool Contains(string text)
        {
            bool contains = ContainsChunk(text);

            if (contains)
                return true;

            return text.Split(' ')
                .All(ContainsChunk);
        }

        private bool ContainsChunk(string text)
        {
            return (FirstName != null && FirstName.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (MiddleName != null && MiddleName.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (LastName != null && LastName.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public static implicit operator PersonName(string text)
        {
            return Parse(text);
        }

        private static PersonName Parse(string text)
        {
            if (text == null)
                return null;

            string[] chunks = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string firstName = chunks.Length >= 1
                ? chunks[0]
                : null;

            string middleName;
            if (chunks.Length >= 3)
            {
                string[] middleNames = chunks
                    .Where((x, i) => i != 0 && i != chunks.Length)
                    .ToArray();

                middleName = string.Join(" ", middleNames);
            }
            else
            {
                middleName = null;
            }

            string lastName = chunks.Length >= 2
                ? chunks[chunks.Length - 1]
                : null;

            return new PersonName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };
        }

        public static implicit operator string(PersonName personName)
        {
            return personName?.ToString();
        }

        public int CompareTo(PersonName other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            int firstNameComparison = string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
            if (firstNameComparison != 0) return firstNameComparison;

            int middleNameComparison = string.Compare(MiddleName, other.MiddleName, StringComparison.Ordinal);
            if (middleNameComparison != 0) return middleNameComparison;

            return string.Compare(LastName, other.LastName, StringComparison.Ordinal);
        }

        public bool Equals(PersonName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(firstName, other.firstName) &&
                   string.Equals(middleName, other.middleName) &&
                   string.Equals(lastName, other.lastName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((PersonName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (firstName != null ? firstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (middleName != null ? middleName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (lastName != null ? lastName.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected virtual void OnFirstNameChanged()
        {
            FirstNameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMiddleNameChanged()
        {
            MiddleNameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLastNameChanged()
        {
            LastNameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}