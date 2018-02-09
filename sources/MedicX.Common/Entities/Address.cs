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
using System.Text;

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Street != null)
                sb.Append(Street);

            if (City != null)
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(City);
            }

            if (County != null)
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(County);
            }

            if (Country != null)
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(Country);
            }

            return sb.ToString();
        }

        public bool Contains(string text)
        {
            return (Street != null && Street.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (City != null && City.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (County != null && County.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Country != null && Country.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}