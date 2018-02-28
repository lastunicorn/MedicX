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
        private string comments;

        public Guid Id { get; set; }

        public PersonName Name
        {
            get => name;
            set
            {
                if (name != null)
                    name.Changed -= HandleNameChanged;

                name = value;

                if (name != null)
                    name.Changed += HandleNameChanged;

                OnNameChanged();
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

        private void HandleNameChanged(object sender, EventArgs eventArgs)
        {
            OnNameChanged();
            OnChanged();
        }

        public virtual void CopyFrom(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Comments = person.Comments;
        }

        public virtual bool Contains(string text)
        {
            bool foundInName = Name != null && Name.Contains(text);

            if (foundInName)
                return true;

            bool foundInComments = Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;

            if (foundInComments)
                return true;

            return false;
        }

        public override string ToString()
        {
            return Name?.ToString();
        }

        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}