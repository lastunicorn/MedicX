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
    public class Address : IEquatable<Address>
    {
        private string street;
        private string city;
        private string county;
        private string country;

        public string Street
        {
            get => street;
            set
            {
                street = value;

                OnStreetChanged();
                OnChanged();
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;

                OnCityChanged();
                OnChanged();
            }
        }

        public string County
        {
            get => county;
            set
            {
                county = value;

                OnCountyChanged();
                OnChanged();
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;

                OnCountryChanged();
                OnChanged();
            }
        }

        public event EventHandler StreetChanged;
        public event EventHandler CityChanged;
        public event EventHandler CountyChanged;
        public event EventHandler CountryChanged;
        public event EventHandler Changed;

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

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(street, other.street) &&
                   string.Equals(city, other.city) &&
                   string.Equals(county, other.county) &&
                   string.Equals(country, other.country);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (street != null ? street.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (city != null ? city.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (county != null ? county.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (country != null ? country.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected virtual void OnStreetChanged()
        {
            StreetChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCityChanged()
        {
            CityChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCountyChanged()
        {
            CountyChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCountryChanged()
        {
            CountryChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}