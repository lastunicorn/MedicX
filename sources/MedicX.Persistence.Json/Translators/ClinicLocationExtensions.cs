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
    internal static class ClinicLocationExtensions
    {
        public static List<ClinicLocation> Translate(this IEnumerable<Entities.ClinicLocation> clinicLocations, Clinic clinic)
        {
            return clinicLocations
                .Select(x => x.Translate(clinic))
                .ToList();
        }

        public static ClinicLocation Translate(this Entities.ClinicLocation clinicLocation, Clinic clinic)
        {
            if (clinicLocation == null)
                return null;

            return new ClinicLocation
            {
                Id = clinicLocation.Id,
                Clinic = clinic,
                Address = clinicLocation.Address.Translate(),
                Phones = clinicLocation.Phones.ToList(),
                Program = clinicLocation.Program,
                Comments = clinicLocation.Comments
            };
        }

        public static List<Entities.ClinicLocation> Translate(this IEnumerable<ClinicLocation> clinicLocations)
        {
            return clinicLocations
                .Select(x => x.Translate())
                .ToList();
        }

        public static Entities.ClinicLocation Translate(this ClinicLocation clinicLocation)
        {
            if (clinicLocation == null)
                return null;

            return new Entities.ClinicLocation
            {
                Id = clinicLocation.Id,
                Address = clinicLocation.Address.Translate(),
                Phones = clinicLocation.Phones.ToList(),
                Program = clinicLocation.Program,
                Comments = clinicLocation.Comments
            };
        }
    }
}