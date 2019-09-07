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

namespace DustInTheWind.MedicX.Persistence.Translators
{
    internal static class PrescriptionsExtensions
    {
        public static List<Prescription> Translate(this IEnumerable<Json.Entities.Prescription> prescriptions)
        {
            return prescriptions?
                .Select(Translate)
                .ToList();
        }

        private static Prescription Translate(this Json.Entities.Prescription prescription)
        {
            if (prescription == null)
                return null;

            return new Prescription
            {
                Description = prescription.Description,
                Result = prescription.Result
            };
        }

        public static List<Json.Entities.Prescription> Translate(this IEnumerable<Prescription> prescriptions)
        {
            return prescriptions?
                .Select(Translate)
                .ToList();
        }

        private static Json.Entities.Prescription Translate(this Prescription prescription)
        {
            if (prescription == null)
                return null;

            return new Json.Entities.Prescription
            {
                Description = prescription.Description,
                Result = prescription.Result
            };
        }
    }

    internal static class InvestigationResultExtensions
    {
        public static List<InvestigationResult> Translate(this IEnumerable<Json.Entities.InvestigationResult> investigationResults)
        {
            return investigationResults?
                .Select(Translate)
                .ToList();
        }

        private static InvestigationResult Translate(this Json.Entities.InvestigationResult investigationResult)
        {
            if (investigationResult == null)
                return null;

            return new InvestigationResult
            {
                InvestigationDescription = null,
                Value = investigationResult.Value
            };
        }

        public static List<Json.Entities.InvestigationResult> Translate(this IEnumerable<InvestigationResult> investigationResults)
        {
            return investigationResults?
                .Select(Translate)
                .ToList();
        }

        private static Json.Entities.InvestigationResult Translate(this InvestigationResult investigationResult)
        {
            if (investigationResult == null)
                return null;

            return new Json.Entities.InvestigationResult
            {
                InvestigationId = 0,
                Value = investigationResult.Value
            };
        }
    }
}