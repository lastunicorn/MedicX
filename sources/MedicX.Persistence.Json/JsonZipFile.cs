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
using System.IO;
using System.IO.Compression;
using DustInTheWind.MedicX.Persistence.Json.Entities;
using Newtonsoft.Json;

namespace DustInTheWind.MedicX.Persistence.Json
{
    /// <summary>
    /// Responsability: Reads and writes the 
    /// </summary>
    public class JsonZipFile
    {
        private readonly string zipFileName;

        private const string MedicsFileName = "medics.json";
        private const string ClinicsFileName = "clinics.json";
        private const string ConsultationsFileName = "consultations.json";
        private const string InvestigationsFileName = "investigations.json";
        private const string InvestigationDescriptionsFileName = "investigation-descriptions.json";

        public MedicXData Data { get; set; }

        public readonly List<Exception> Warnings = new List<Exception>();

        public JsonZipFile(string zipFileName)
        {
            this.zipFileName = zipFileName ?? throw new ArgumentNullException(nameof(zipFileName));
        }

        public void Open()
        {
            Warnings.Clear();
            Data = new MedicXData();

            if (!File.Exists(zipFileName))
                return;

            using (FileStream fileStream = File.OpenRead(zipFileName))
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    switch (entry.Name)
                    {
                        case MedicsFileName:
                            Data.Medics = ReadFile<List<Medic>>(entry);
                            break;

                        case ClinicsFileName:
                            Data.Clinics = ReadFile<List<Clinic>>(entry);
                            break;

                        case ConsultationsFileName:
                            Data.Consultations = ReadFile<List<Consultation>>(entry);
                            break;

                        case InvestigationsFileName:
                            Data.Investigations = ReadFile<List<Investigation>>(entry);
                            break;

                        case InvestigationDescriptionsFileName:
                            Data.InvestigationDescriptions = ReadFile<List<InvestigationDescription>>(entry);
                            break;

                        default:
                            Exception warning = new Exception($"Invalid file encountered: {entry.Name}");
                            Warnings.Add(warning);
                            break;
                    }
                }
            }
        }

        private static T ReadFile<T>(ZipArchiveEntry entry)
        {
            using (Stream stream = entry.Open())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                return jsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }

        public void Save()
        {
            using (FileStream fileStream = File.Create(zipFileName))
            {
                using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    AddFile(archive, Data.Medics, MedicsFileName);
                    AddFile(archive, Data.Clinics, ClinicsFileName);
                    AddFile(archive, Data.Consultations, ConsultationsFileName);
                    AddFile(archive, Data.Investigations, InvestigationsFileName);
                    AddFile(archive, Data.InvestigationDescriptions, InvestigationDescriptionsFileName);
                }
            }
        }

        private static void AddFile(ZipArchive archive, object o, string fileName)
        {
            ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(fileName);

            using (Stream stream = zipArchiveEntry.Open())
            using (StreamWriter streamWriter = new StreamWriter(stream))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                JsonSerializer jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                jsonSerializer.Serialize(jsonTextWriter, o);
            }
        }
    }
}