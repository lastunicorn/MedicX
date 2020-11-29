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
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Persistence.JsonStorage.Translators
{
    internal static class InvestigationTypesExtensions
    {
        public static List<InvestigationTest> Translate(this IEnumerable<Entities.InvestigationDescription> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static InvestigationTest Translate(this Entities.InvestigationDescription investigationDescription)
        {
            if (investigationDescription == null)
                return null;

            return new InvestigationTest
            {
                Id = investigationDescription.Id,
                Name = investigationDescription.Name,
                Substance = investigationDescription.Substance,
                Method = investigationDescription.Method,
                Comments = investigationDescription.Comments,
                Items = investigationDescription.Items.Translate()
            };
        }

        public static List<Entities.InvestigationDescription> Translate(this IEnumerable<InvestigationTest> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static Entities.InvestigationDescription Translate(this InvestigationTest investigationTest)
        {
            if (investigationTest == null)
                return null;

            return new Entities.InvestigationDescription
            {
                Id = investigationTest.Id,
                Name = investigationTest.Name,
                Substance = investigationTest.Substance,
                Method = investigationTest.Method,
                Comments = investigationTest.Comments,
                Items = investigationTest.Items.Translate()
            };
        }
    }
}