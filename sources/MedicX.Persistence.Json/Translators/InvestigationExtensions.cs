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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Json.Translators
{
    internal static class InvestigationExtensions
    {
        public static List<Investigation> Translate(this IEnumerable<Entities.Investigation> investigations, List<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            return investigations?
                .Select(x => Translate((Entities.Investigation) x, medics, clinicLocations))
                .ToList();
        }

        private static Investigation Translate(this Entities.Investigation investigation, IEnumerable<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            if (investigation == null)
                return null;

            return new Investigation
            {
                Date = investigation.Date,
                SentBy = medics.FirstOrDefault(x => x.Id == investigation.SentById),
                Clinic = clinicLocations.FirstOrDefault(x => x.Id == investigation.ClinicLocationId),
                Labels = investigation.Labels?.ToList(),
                Comments = investigation.Comments
            };
        }

        public static List<Entities.Investigation> Translate(this IEnumerable<Investigation> investigations)
        {
            return investigations?
                .Select(x => x.Translate())
                .ToList();
        }

        private static Entities.Investigation Translate(this Investigation investigation)
        {
            if (investigation == null)
                return null;

            return new Entities.Investigation
            {
                Date = investigation.Date,
                SentById = investigation.SentBy.Id,
                ClinicLocationId = investigation.Clinic.Id,
                Labels = investigation.Labels?.ToList(),
                Comments = investigation.Comments
            };
        }
    }
}