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
    internal static class InvestigationTypesItemExtensions
    {
        public static List<InvestigationTypeItem> Translate(this IEnumerable<Entities.InvestigationTypeItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static InvestigationTypeItem Translate(this Entities.InvestigationTypeItem investigationTypeItem)
        {
            if (investigationTypeItem == null)
                return null;

            return new InvestigationTypeItem
            {
                Id = investigationTypeItem.Id,
                Name = investigationTypeItem.Name,
                MeasurementUnit = investigationTypeItem.MeasurementUnit,
                MinValue = investigationTypeItem.MinValue,
                MaxValue = investigationTypeItem.MaxValue
            };
        }

        public static List<Entities.InvestigationTypeItem> Translate(this IEnumerable<InvestigationTypeItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static Entities.InvestigationTypeItem Translate(this InvestigationTypeItem investigationTypeItem)
        {
            if (investigationTypeItem == null)
                return null;

            return new Entities.InvestigationTypeItem
            {
                Id = investigationTypeItem.Id,
                Name = investigationTypeItem.Name,
                MeasurementUnit = investigationTypeItem.MeasurementUnit,
                MinValue = investigationTypeItem.MinValue,
                MaxValue = investigationTypeItem.MaxValue
            };
        }
    }
}