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

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Person
    {
        private PersonName name;
        public int Id { get; set; }

        public PersonName Name
        {
            get => name;
            set
            {
                if(name != null)
                    name.Changed -= HandleNameChanged;

                name = value;

                if (name != null)
                    name.Changed += HandleNameChanged;

                OnNameChanged();
            }
        }

        private void HandleNameChanged(object sender, EventArgs eventArgs)
        {
            OnNameChanged();
        }

        public string Comments { get; set; }

        public event EventHandler NameChanged;

        public virtual void CopyFrom(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Comments = person.Comments;
        }

        public override string ToString()
        {
            return Name?.ToString();
        }

        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}