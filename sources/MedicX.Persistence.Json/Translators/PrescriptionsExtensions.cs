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
    internal static class PrescriptionsExtensions
    {
        public static List<Prescription> Translate(this IEnumerable<Entities.Prescription> prescriptions)
        {
            return prescriptions
                .Select(Translate)
                .ToList();
        }

        private static Prescription Translate(this Entities.Prescription prescription)
        {
            return new Prescription
            {
                Description = prescription.Description,
                Result = prescription.Result
            };
        }

        public static List<Entities.Prescription> Translate(this IEnumerable<Prescription> prescriptions)
        {
            return prescriptions
                .Select(Translate)
                .ToList();
        }

        private static Entities.Prescription Translate(this Prescription prescription)
        {
            return new Entities.Prescription
            {
                Description = prescription.Description,
                Result = prescription.Result
            };
        }
    }
}