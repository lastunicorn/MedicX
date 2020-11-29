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
using DustInTheWind.MedicX.Persistence.JsonStorage.Entities;
using DustInTheWind.MedicX.Persistence.JsonStorage.Translators;
using Clinic = DustInTheWind.MedicX.Domain.Entities.Clinic;
using Consultation = DustInTheWind.MedicX.Domain.Entities.Consultation;
using Investigation = DustInTheWind.MedicX.Domain.Entities.Investigation;
using Medic = DustInTheWind.MedicX.Domain.Entities.Medic;

namespace DustInTheWind.MedicX.Persistence.JsonStorage
{
    internal class Storage
    {
        private readonly JsonZipFile jsonZipFile;

        public List<Medic> Medics { get; set; }
        
        public List<Clinic> Clinics { get; set; }
        
        public List<Consultation> Consultations { get; set; }
        
        public List<Investigation> Investigations { get; set; }

        public Storage(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            jsonZipFile = new JsonZipFile(connectionString);
        }

        public void Open()
        {
            jsonZipFile.Open();

            Medics = jsonZipFile.Data.Medics.Translate() ?? new List<Medic>();
            Clinics = jsonZipFile.Data.Clinics.Translate() ?? new List<Clinic>();
            Consultations = jsonZipFile.Data.Consultations.Translate(Medics, Clinics) ?? new List<Consultation>();
            Investigations = jsonZipFile.Data.Investigations.Translate(Medics, Clinics) ?? new List<Investigation>();
        }

        public void Save()
        {
            jsonZipFile.Data = new MedicXData
            {
                Medics = Medics.Translate(),
                Clinics = Clinics.Translate(),
                Consultations = Consultations.Translate(),
                Investigations = Investigations.Translate(),
                InvestigationDescriptions = null
            };

            jsonZipFile.Save();
        }
    }
}