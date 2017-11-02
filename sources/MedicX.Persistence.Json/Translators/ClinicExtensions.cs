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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Json.Translators
{
    internal static class ClinicExtensions
    {
        public static List<Clinic> Translate(this IEnumerable<Entities.Clinic> clinics)
        {
            return clinics
                .Select(Translate)
                .ToList();
        }

        public static Clinic Translate(this Entities.Clinic clinic)
        {
            if (clinic == null)
                return null;

            Clinic clinicBl = new Clinic
            {
                Name = clinic.Name,
                Comments = clinic.Comments
            };

            clinicBl.Locations = clinic.Locations.Translate(clinicBl);

            return clinicBl;
        }

        public static List<Entities.Clinic> Translate(this IEnumerable<Clinic> clinics)
        {
            return clinics
                .Select(Translate)
                .ToList();
        }

        public static Entities.Clinic Translate(this Clinic clinic)
        {
            if (clinic == null)
                return null;

            return new Entities.Clinic
            {
                Name = clinic.Name,
                Comments = clinic.Comments,
                Locations = clinic.Locations.Translate()
            };
        }
    }
}