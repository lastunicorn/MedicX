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
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Persistence
{
    internal class MedicRepository : IMedicRepository
    {
        private readonly Storage storage;

        public MedicRepository(Storage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public List<Medic> GetAll()
        {
            return storage.Medics;
        }

        public Medic GetById(Guid id)
        {
            return storage.Medics
                .Where(x => x != null)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Medic medic)
        {
            bool idExists = storage.Medics
                .Any(x => x.Id == medic.Id);

            if (idExists)
                throw new ApplicationException("Another medic with the same id already exists.");

            storage.Medics.Add(medic);
        }

        public void AddOrUpdate(Medic medic)
        {
            if (medic == null)
                return;

            Medic existingMedic = storage.Medics
                .Where(x => x != null)
                .FirstOrDefault(x => x.Id == medic.Id);

            if (existingMedic == null)
                Add(medic);
            else
                existingMedic.CopyFrom(medic);
        }

        public List<Medic> GetByName(string medicName)
        {
            return storage.Medics
                .Where(x => x != null)
                .Where(x => x.Name != null && x.Name.Contains(medicName))
                .ToList();
        }

        public List<Medic> Search(string text)
        {
            return storage.Medics
                .Where(x => x != null)
                .Where(x => (x.Name != null && x.Name.Contains(text)) ||
                            (x.Specializations.Any(z => z != null && z.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                            (x.Comments != null && x.Comments.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0))
                .ToList();
        }
    }
}