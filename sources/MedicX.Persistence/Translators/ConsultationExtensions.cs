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
    internal static class ConsultationExtensions
    {
        public static List<Consultation> Translate(this IEnumerable<Json.Entities.Consultation> consultations, List<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            return consultations?
                .Select(x => x.Translate(medics, clinicLocations))
                .ToList();
        }

        private static Consultation Translate(this Json.Entities.Consultation consultation, IEnumerable<Medic> medics, IEnumerable<Clinic> clinicLocations)
        {
            if (consultation == null)
                return null;

            return new Consultation
            {
                Id = consultation.Id,
                Date = consultation.Date,
                Medic = medics.FirstOrDefault(x => x.Id == consultation.MedicId),
                Clinic = clinicLocations.FirstOrDefault(x => x.Id == consultation.ClinicLocationId),
                Labels = consultation.Labels == null
                    ? null
                    : new ObservableCollection<string>(consultation.Labels.ToList()),
                Comments = consultation.Comments,
                Prescriptions = consultation.Prescriptions == null
                    ? null
                    : new ObservableCollection<Prescription>(consultation.Prescriptions.Translate())
            };
        }

        public static List<Json.Entities.Consultation> Translate(this IEnumerable<Consultation> consultations)
        {
            return consultations?
                .Select(x => x.Translate())
                .ToList();
        }

        private static Json.Entities.Consultation Translate(this Consultation consultation)
        {
            if (consultation == null)
                return null;

            return new Json.Entities.Consultation
            {
                Id = consultation.Id,
                Date = consultation.Date,
                MedicId = consultation.Medic?.Id,
                ClinicLocationId = consultation.Clinic?.Id,
                Labels = consultation.Labels?.ToList(),
                Comments = consultation.Comments,
                Prescriptions = consultation.Prescriptions?.Translate()
            };
        }
    }
}