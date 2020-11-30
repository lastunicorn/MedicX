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
using Newtonsoft.Json;

namespace DustInTheWind.MedicX.Persistence.JsonStorage.Entities
{
    public class JsonMedicalEvent
    {
        [JsonProperty("id", Order = 1)]
        public Guid Id { get; set; }

        [JsonProperty("date", Order = 1)]
        public DateTime Date { get; set; }

        [JsonProperty("medic", Order = 1)]
        public Guid? MedicId { get; set; }

        [JsonProperty("location", Order = 1)]
        public Guid? ClinicLocationId { get; set; }

        [JsonProperty("labels", Order = 1)]
        public List<string> Labels { get; set; }

        [JsonProperty("files", Order = 1)]
        public List<string> Files { get; set; }

        [JsonProperty("comments", Order = 1)]
        public string Comments { get; set; }
    }
}