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
    internal class MedicRepository : IMedicRepository
    {
        private readonly MedicXData medicXData;

        public MedicRepository(MedicXData medicXData)
        {
            if (medicXData == null) throw new ArgumentNullException(nameof(medicXData));
            this.medicXData = medicXData;
        }

        public List<Medic> GetAll()
        {
            return medicXData.Medics;
        }

        public Medic GetById(int id)
        {
            return medicXData.Medics
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Medic medic)
        {
            int id = 0;

            while (true)
            {
                id++;

                bool idExists = medicXData.Medics
                    .Any(x => x.Id == id);

                if (!idExists)
                    break;
            }

            medic.Id = id;

            medicXData.Medics.Add(medic);
        }

        public List<Medic> GetByName(string medicName)
        {
            return medicXData.Medics
                .Where(x => x.Name != null && x.Name.Contains(medicName))
                .ToList();
        }

        public List<Medic> Search(string text)
        {
            return medicXData.Medics
                .Where(x => (x.Name != null && x.Name.Contains(text)) ||
                    (x.Specializations.Any(z => z != null && z.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                    (x.Comments != null && x.Comments.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0))
                .ToList();
        }
    }
}
