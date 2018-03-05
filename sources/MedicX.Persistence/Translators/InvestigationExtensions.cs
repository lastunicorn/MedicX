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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Translators
{
    internal static class InvestigationExtensions
    {
        public static List<Investigation> Translate(this IEnumerable<Json.Entities.Investigation> investigations, List<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            return investigations?
                .Select(x => x.Translate(medics, clinicLocations))
                .ToList();
        }

        private static Investigation Translate(this Json.Entities.Investigation investigation, IEnumerable<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            if (investigation == null)
                return null;

            return new Investigation
            {
                Id = investigation.Id,
                Date = investigation.Date,
                SentBy = medics.FirstOrDefault(x => x.Id == investigation.SentById),
                Medic = medics.FirstOrDefault(x => x.Id == investigation.MedicId),
                Clinic = clinicLocations.FirstOrDefault(x => x.Id == investigation.ClinicLocationId),
                Labels = investigation.Labels == null
                    ? null
                    : new ObservableCollection<string>(investigation.Labels.ToList()),
                Comments = investigation.Comments,
                Result = investigation.Result == null
                    ? null
                    : new ObservableCollection<InvestigationResult>(investigation.Result.Translate())
            };
        }

        public static List<Json.Entities.Investigation> Translate(this IEnumerable<Investigation> investigations)
        {
            return investigations?
                .Select(x => x.Translate())
                .ToList();
        }

        private static Json.Entities.Investigation Translate(this Investigation investigation)
        {
            if (investigation == null)
                return null;

            return new Json.Entities.Investigation
            {
                Id = investigation.Id,
                Date = investigation.Date,
                SentById = investigation.SentBy?.Id,
                MedicId = investigation.Medic?.Id,
                ClinicLocationId = investigation.Clinic?.Id,
                Labels = investigation.Labels?.ToList(),
                Comments = investigation.Comments,
                Result = investigation.Result?.Translate()
            };
        }
    }
}