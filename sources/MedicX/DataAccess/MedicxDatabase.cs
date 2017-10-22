using System.Collections.Generic;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace DustInTheWind.MedicX.DataAccess
{
    internal class MedicXDatabase
    {
        [JsonProperty("medics")]
        public List<Medic> Medics { get; set; }

        [JsonProperty("clinics")]
        public List<Clinic> Clinics { get; set; }
    }

    internal class Clinic
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("locations")]
        public List<ClinicLocation> Locations { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }

    internal class ClinicLocation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    internal class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    internal class Person
    {
        [JsonProperty("id", Order = 1)]
        public int Id { get; set; }

        [JsonProperty("name", Order = 1)]
        public PersonName Name { get; set; }

        [JsonProperty("comments", Order = 1)]
        public string Comments { get; set; }
    }

    internal class PersonName
    {
        [JsonProperty("first")]
        public string FirstName { get; set; }

        [JsonProperty("middle")]
        public string MiddleName { get; set; }

        [JsonProperty("last")]
        public string LastName { get; set; }
    }

    internal class Medic : Person
    {
        [JsonProperty("specializations", Order = 2)]
        public List<string> Specializations { get; set; }
    }
}