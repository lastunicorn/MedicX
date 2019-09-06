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
    internal class ConsultationRepository : IConsultationRepository
    {
        private readonly Storage storage;

        public ConsultationRepository(Storage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public List<Consultation> GetAll()
        {
            return storage.Consultations
                .Where(x => x != null)
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public List<Consultation> Search(string text)
        {
            return storage.Consultations
                .Where(x => x != null)
                .Where(x => (x.Medic?.Name != null && x.Medic.Name.Contains(text)) ||
                            (x.Clinic?.Name != null && x.Clinic.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (x.Labels != null && x.Labels.Any(z => z != null && z.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                            (x.Comments != null && x.Comments.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0))
                .ToList();
        }

        public void AddOrUpdate(Consultation consultation)
        {
            if (consultation == null)
                return;

            Consultation existingConsultation = storage.Consultations
                .Where(x => x != null)
                .FirstOrDefault(x => x.Id == consultation.Id);

            if (existingConsultation == null)
                storage.Consultations.Add(consultation);
            else
                existingConsultation.CopyFrom(consultation);
        }
    }
}