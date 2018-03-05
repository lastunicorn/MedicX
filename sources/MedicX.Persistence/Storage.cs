﻿// MedicX
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
using DustInTheWind.MedicX.Persistence.Json;
using DustInTheWind.MedicX.Persistence.Json.Entities;
using DustInTheWind.MedicX.Persistence.Translators;
using Clinic = DustInTheWind.MedicX.Common.Entities.Clinic;
using Consultation = DustInTheWind.MedicX.Common.Entities.Consultation;
using Investigation = DustInTheWind.MedicX.Common.Entities.Investigation;
using Medic = DustInTheWind.MedicX.Common.Entities.Medic;

namespace DustInTheWind.MedicX.Persistence
{
    public class Storage
    {
        private readonly JsonZipFile jsonZipFile;

        public List<Medic> Medics { get; set; }
        public List<Clinic> Clinics { get; set; }
        public List<Consultation> Consultations { get; set; }
        public List<Investigation> Investigations { get; set; }

        public Storage()
        {
            jsonZipFile = new JsonZipFile("medicx.zip");
        }

        public void Open()
        {
            jsonZipFile.Open();

            Medics = jsonZipFile.Data.Medics.Translate();
            Clinics = jsonZipFile.Data.Clinics.Translate();
            Consultations = jsonZipFile.Data.Consultations.Translate(Medics, Clinics);
            Investigations = jsonZipFile.Data.Investigations.Translate(Medics, Clinics);
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