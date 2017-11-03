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
    internal static class InvestigationTypesExtensions
    {
        public static List<InvestigationType> Translate(this IEnumerable<Entities.InvestigationType> investigationTypes)
        {
            return investigationTypes
                .Select(Translate)
                .ToList();
        }

        public static InvestigationType Translate(this Entities.InvestigationType investigationType)
        {
            if (investigationType == null)
                return null;

            return new InvestigationType
            {
                Id = investigationType.Id,
                Name = investigationType.Name,
                Substance = investigationType.Substance,
                Method = investigationType.Method,
                Comments = investigationType.Comments,
                MeasurementUnit = investigationType.MeasurementUnit,
                MinValue = investigationType.MinValue,
                MaxValue = investigationType.MaxValue
            };
        }

        public static List<Entities.InvestigationType> Translate(this IEnumerable<InvestigationType> investigationTypes)
        {
            return investigationTypes
                .Select(Translate)
                .ToList();
        }

        public static Entities.InvestigationType Translate(this InvestigationType investigationType)
        {
            if (investigationType == null)
                return null;

            return new Entities.InvestigationType
            {
                Id = investigationType.Id,
                Name = investigationType.Name,
                Substance = investigationType.Substance,
                Method = investigationType.Method,
                Comments = investigationType.Comments,
                MeasurementUnit = investigationType.MeasurementUnit,
                MinValue = investigationType.MinValue,
                MaxValue = investigationType.MaxValue
            };
        }
    }
}