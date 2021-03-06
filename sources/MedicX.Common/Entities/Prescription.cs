﻿// MedicX
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
    public class Prescription : IEquatable<Prescription>
    {
        private string description;
        private object result;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnChanged();
            }
        }

        public object Result
        {
            get => result;
            set
            {
                result = value;
                OnChanged();
            }
        }

        public event EventHandler Changed;

        public bool Contains(string text)
        {
            return (Description != null && Description.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public bool Equals(Prescription other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(description, other.description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Prescription)obj);
        }

        public override int GetHashCode()
        {
            return (description != null ? description.GetHashCode() : 0);
        }
    }
}