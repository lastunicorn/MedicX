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
using Newtonsoft.Json;

namespace DustInTheWind.MedicX.Persistence.Json.Entities
{
    public class MedicXData
    {
        [JsonProperty("medics")]
        public List<Medic> Medics { get; set; }

        [JsonProperty("clinics")]
        public List<Clinic> Clinics { get; set; }

        [JsonProperty("consultations")]
        public List<Consultation> Consultations { get; set; }

        [JsonProperty("investigations")]
        public List<Investigation> Investigations { get; set; }

        [JsonProperty("investigationDescriptions")]
        public List<InvestigationDescription> InvestigationDescriptions { get; set; }
    }
}