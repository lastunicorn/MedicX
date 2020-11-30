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
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Application.GetAllMedics
{
    public class MedicDto : IEquatable<MedicDto>
    {
        public Guid Id { get; set; }
        
        public PersonName Name { get; set; }
        
        public List<string> Specializations { get; set; }
        
        public string Comments { get; set; }

        public MedicDto(Medic medic)
        {
            if (medic != null)
            {
                Id = medic.Id;
                Name = medic.Name;
                Specializations = medic.Specializations.ToList();
                Comments = medic.Comments;
            }
        }

        public bool Contains(string text)
        {
            bool foundInName = Name != null && Name.Contains(text);

            if (foundInName)
                return true;

            bool foundInComments = Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;

            if (foundInComments)
                return true;

            return Specializations != null && Specializations.Any(x => x != null && x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public bool Equals(MedicDto other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id.Equals(other.Id) && Equals(Name, other.Name) && Equals(Specializations, other.Specializations) && string.Equals(Comments, other.Comments);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((MedicDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Specializations != null ? Specializations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Comments != null ? Comments.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}