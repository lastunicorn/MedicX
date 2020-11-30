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
    internal static class TestItemExtensions
    {
        public static List<TestItem> Translate(this IEnumerable<JsonTestItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static TestItem Translate(this JsonTestItem testItem)
        {
            if (testItem == null)
                return null;

            return new TestItem
            {
                Id = testItem.Id,
                Name = testItem.Name,
                MeasurementUnit = testItem.MeasurementUnit,
                MinValue = testItem.MinValue,
                MaxValue = testItem.MaxValue,
                Value = testItem.Value,
                Comments = testItem.Comments
            };
        }

        public static List<JsonTestItem> Translate(this IEnumerable<TestItem> investigationTypeItems)
        {
            return investigationTypeItems?
                .Select(Translate)
                .ToList();
        }

        public static JsonTestItem Translate(this TestItem testItem)
        {
            if (testItem == null)
                return null;

            return new JsonTestItem
            {
                Id = testItem.Id,
                Name = testItem.Name,
                MeasurementUnit = testItem.MeasurementUnit,
                MinValue = testItem.MinValue,
                MaxValue = testItem.MaxValue,
                Value = testItem.Value,
                Comments = testItem.Comments
            };
        }
    }
}