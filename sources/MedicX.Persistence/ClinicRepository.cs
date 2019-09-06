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
using DustInTheWind.MedicX.Common.DataAccess;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence
{
    internal class ClinicRepository : IClinicRepository
    {
        private readonly Storage storage;

        public ClinicRepository(Storage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public List<Clinic> GetAll()
        {
            return storage.Clinics;
        }

        public Clinic GetById(Guid id)
        {
            return storage.Clinics
                .Where(x => x != null)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Clinic> GetByName(string clinicName)
        {
            return storage.Clinics
                .Where(x => x != null)
                .Where(x => x.Name != null && x.Name.IndexOf(clinicName, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<Clinic> Search(string text)
        {
            return storage.Clinics
                .Where(x => x != null)
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

            Clinic existingClinic = storage.Clinics
                .Where(x => x != null)
                .FirstOrDefault(x => x.Id == clinic.Id);

            if (existingClinic == null)
                storage.Clinics.Add(clinic);
            else
                existingClinic.CopyFrom(clinic);
        }
    }
}