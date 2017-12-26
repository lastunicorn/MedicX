// MedicX
// Copyright (C) 2017 Dust in the Wind
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
    public class PersonName
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

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

            string[] chunks = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

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
    }
}