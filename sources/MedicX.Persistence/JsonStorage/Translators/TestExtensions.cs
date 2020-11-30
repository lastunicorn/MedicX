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
using DustInTheWind.MedicX.Persistence.JsonStorage.Entities;

namespace DustInTheWind.MedicX.Persistence.JsonStorage.Translators
{
    internal static class TestExtensions
    {
        public static List<Test> Translate(this IEnumerable<JsonTest> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static Test Translate(this JsonTest test)
        {
            if (test == null)
                return null;

            return new Test
            {
                Id = test.Id,
                Name = test.Name,
                Substance = test.Substance,
                Method = test.Method,
                Comments = test.Comments,
                Items = test.Items.Translate()
            };
        }

        public static List<JsonTest> Translate(this IEnumerable<Test> investigationTypes)
        {
            return investigationTypes?
                .Select(Translate)
                .ToList();
        }

        public static JsonTest Translate(this Test test)
        {
            if (test == null)
                return null;

            return new JsonTest
            {
                Id = test.Id,
                Name = test.Name,
                Substance = test.Substance,
                Method = test.Method,
                Comments = test.Comments,
                Items = test.Items.Translate()
            };
        }
    }
}