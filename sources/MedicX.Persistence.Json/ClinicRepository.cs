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

using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Json
{
    internal class ClinicRepository : IClinicRepository
    {
        private readonly MedicXData medicXData;

        public ClinicRepository(MedicXData medicXData)
        {
            this.medicXData = medicXData ?? throw new ArgumentNullException(nameof(medicXData));
        }

        public List<Clinic> GetAll()
        {
            return medicXData.Clinics;
        }

        public Clinic GetById(Guid id)
        {
            return medicXData.Clinics
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Clinic> GetByName(string clinicName)
        {
            return medicXData.Clinics
                .Where(x => x.Name != null && x.Name.IndexOf(clinicName, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<Clinic> Search(string text)
        {
            return medicXData.Clinics
                .Where(x => (x.Name != null && x.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (x.Phones != null && x.Phones.Any(z => z != null && z.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                    (x.Address != null && x.Address.Contains(text)) ||
                    (x.Comments != null && x.Comments.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0))
                .ToList();
        }

        public void AddOrUpdate(Clinic clinic)
        {
            if (clinic == null)
                return;

            Clinic existingClinic = medicXData.Clinics
                .FirstOrDefault(x => x.Id == clinic.Id);

            if (existingClinic == null)
                medicXData.Clinics.Add(clinic);
            else
                existingClinic.CopyFrom(clinic);
        }
    }
}