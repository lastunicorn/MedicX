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
    internal static class InvestigationTypesExtensions
    {
        public static List<InvestigationDescription> Translate(this IEnumerable<Entities.InvestigationDescription> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static InvestigationDescription Translate(this Entities.InvestigationDescription investigationDescription)
        {
            if (investigationDescription == null)
                return null;

            return new InvestigationDescription
            {
                Id = investigationDescription.Id,
                Name = investigationDescription.Name,
                Substance = investigationDescription.Substance,
                Method = investigationDescription.Method,
                Comments = investigationDescription.Comments,
                Items = investigationDescription.Items.Translate()
            };
        }

        public static List<Entities.InvestigationDescription> Translate(this IEnumerable<InvestigationDescription> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static Entities.InvestigationDescription Translate(this InvestigationDescription investigationDescription)
        {
            if (investigationDescription == null)
                return null;

            return new Entities.InvestigationDescription
            {
                Id = investigationDescription.Id,
                Name = investigationDescription.Name,
                Substance = investigationDescription.Substance,
                Method = investigationDescription.Method,
                Comments = investigationDescription.Comments,
                Items = investigationDescription.Items.Translate()
            };
        }
    }
}