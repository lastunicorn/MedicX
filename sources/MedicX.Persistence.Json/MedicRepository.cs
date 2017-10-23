// MedicX
// Copyright (C) 2017 Dust in the Wind
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
using System.IO;
using DustInTheWind.MedicX.Persistence.Json.Entities;
using DustInTheWind.MedicX.Persistence.Json.Translators;
using Newtonsoft.Json;
using Medic = DustInTheWind.MedicX.Common.Entities.Medic;

namespace DustInTheWind.MedicX.Persistence.Json
{
    public class MedicRepository
    {
        private const string JsonFileName = "medicx.json";

        private MedicXDatabase database;

        public MedicRepository()
        {
            OpenDatabase();
        }

        private void OpenDatabase()
        {
            if (!File.Exists(JsonFileName))
            {
                database = new MedicXDatabase();
                return;
            }

            string json = File.ReadAllText(JsonFileName);
            database = JsonConvert.DeserializeObject(json, typeof(MedicXDatabase)) as MedicXDatabase;
        }

        public List<Medic> GetAll()
        {
            return database.Medics.Translate();
        }

        public void Save()
        {
            SaveDatabase();
        }

        private void SaveDatabase()
        {
            using (StreamWriter streamWriter = new StreamWriter("medicx2.json"))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,

                };

                JsonSerializer jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                jsonSerializer.Serialize(jsonTextWriter, database);
            }
        }
    }
}
