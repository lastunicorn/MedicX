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

namespace DustInTheWind.MedicX.Persistence.Translators
{
    internal static class InvestigationTypesItemExtensions
    {
        public static List<InvestigationItem> Translate(this IEnumerable<Json.Entities.InvestigationItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static InvestigationItem Translate(this Json.Entities.InvestigationItem investigationItem)
        {
            if (investigationItem == null)
                return null;

            return new InvestigationItem
            {
                Id = investigationItem.Id,
                Name = investigationItem.Name,
                MeasurementUnit = investigationItem.MeasurementUnit,
                MinValue = investigationItem.MinValue,
                MaxValue = investigationItem.MaxValue
            };
        }

        public static List<Json.Entities.InvestigationItem> Translate(this IEnumerable<InvestigationItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static Json.Entities.InvestigationItem Translate(this InvestigationItem investigationItem)
        {
            if (investigationItem == null)
                return null;

            return new Json.Entities.InvestigationItem
            {
                Id = investigationItem.Id,
                Name = investigationItem.Name,
                MeasurementUnit = investigationItem.MeasurementUnit,
                MinValue = investigationItem.MinValue,
                MaxValue = investigationItem.MaxValue
            };
        }
    }
}