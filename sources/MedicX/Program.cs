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

using System.IO;
using DustInTheWind.MedicX.DataAccess;
using Newtonsoft.Json;

namespace DustInTheWind.MedicX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var json = File.ReadAllText("medicx.json");
            var medicx = JsonConvert.DeserializeObject(json, typeof(MedicXDatabase));

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var json2 = JsonConvert.SerializeObject(medicx, Formatting.Indented, jsonSerializerSettings);
            File.WriteAllText("medicx2.json", json2);
        }
    }
}