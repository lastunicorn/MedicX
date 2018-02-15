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
    internal class InvestigationRepository : IInvestigationRepository
    {
        private readonly MedicXData medicXData;

        public InvestigationRepository(MedicXData medicXData)
        {
            this.medicXData = medicXData ?? throw new ArgumentNullException(nameof(medicXData));
        }

        public List<Investigation> GetAll()
        {
            return medicXData.Investigations
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public List<Investigation> Search(string text)
        {
            return medicXData.Investigations
                .Where(x => (x.SentBy?.Name != null && x.SentBy.Name.Contains(text)) ||
                            (x.Clinic?.Name != null && x.Clinic.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (x.Labels != null && x.Labels.Any(z => z != null && z.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)) ||
                            (x.Comments != null && x.Comments.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0))
                .ToList();
        }

        public void AddOrUpdate(Investigation investigation)
        {
            if (investigation == null)
                return;

            Investigation existingInvestigation = medicXData.Investigations
                .FirstOrDefault(x => x.Id == investigation.Id);

            if (existingInvestigation == null)
                medicXData.Investigations.Add(investigation);
            else
                existingInvestigation.CopyFrom(investigation);
        }
    }
}