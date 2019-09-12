using System;

namespace DustInTheWind.MedicX.Application.GetAllClinics
{
    public struct Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Program { get; set; }
        public string Comments { get; set; }
    }
}