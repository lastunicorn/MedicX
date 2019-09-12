namespace DustInTheWind.MedicX.Application.GetAllClinics
{
    public struct Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public Address(Domain.Entities.Address address)
        {
            if (address != null)
            {
                Street = address.Street;
                City = address.City;
                County = address.County;
                Country = address.Country;
            }
            else
            {
                Street = null;
                City = null;
                County = null;
                Country = null;
            }
        }
    }
}