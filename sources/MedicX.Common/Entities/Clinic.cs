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
using System.Linq;

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Clinic
    {
        private string name;

        public Guid Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnNameChanged();
            }
        }

        public Address Address { get; set; }

        public List<string> Phones { get; set; }

        public string Program { get; set; }

        public string Comments { get; set; }

        public event EventHandler NameChanged;

        public void CopyFrom(Clinic clinic)
        {
            Id = clinic.Id;
            Name = clinic.Name;
            Address = clinic.Address;
            Phones = clinic.Phones.ToList();
            Program = clinic.Program;
            Comments = clinic.Comments;
        }

        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Contains(string text)
        {
            return (Name != null && Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Address != null && Address.Contains(text)) ||
                   (Phones != null && Phones.Any(x => x != null && x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                   (Program != null && Program.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}