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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Translators
{
    internal static class MedicXDataExtensions
    {
        public static MedicXData Translate(this Json.Entities.MedicXData medicXData)
        {
            if (medicXData == null)
                return null;

            List<Medic> medics = medicXData.Medics.Translate();
            List<Clinic> clinics = medicXData.Clinics.Translate();
            List<Consultation> consultations = medicXData.Consultations.Translate(medics, clinics);
            List<Investigation> investigations = medicXData.Investigations.Translate(medics, clinics);

            return new MedicXData
            {
                Medics = medics,
                Clinics = clinics,
                Consultations = consultations,
                Investigations = investigations
            };
        }

        public static Json.Entities.MedicXData Translate(this MedicXData medicXData)
        {
            if (medicXData == null)
                return null;

            return new Json.Entities.MedicXData
            {
                Medics = medicXData.Medics.Translate(),
                Clinics = medicXData.Clinics.Translate(),
                Consultations = medicXData.Consultations.Translate(),
                Investigations = medicXData.Investigations.Translate(),
                InvestigationDescriptions = null
            };
        }
    }
}