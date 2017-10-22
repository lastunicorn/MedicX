using System;
using System.IO;
using DustInTheWind.MedicX.DataAccess;
using Newtonsoft.Json;

namespace DustInTheWind.MedicX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string json = File.ReadAllText("medicx.json");
            var medicx = JsonConvert.DeserializeObject(json, typeof(MedicXDatabase));

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string json2 = JsonConvert.SerializeObject(medicx, Formatting.Indented, jsonSerializerSettings);
            File.WriteAllText("medicx2.json", json2);
        }
    }
}