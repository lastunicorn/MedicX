using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.GetAllClinics
{
    internal class GetAllClinicsRequestHandler : IRequestHandler<GetAllClinicsRequest, List<Clinic>>
    {
        private readonly MedicXApplication medicXApplication;

        public GetAllClinicsRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task<List<Clinic>> Handle(GetAllClinicsRequest request)
        {
            List<Clinic> clinics = medicXApplication.CurrentProject?.Clinics
                .Select(x => new Clinic
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = new Address(x.Address),
                    Program = x.Program,
                    Comments = x.Comments
                })
                .ToList();

            return Task.FromResult(clinics ?? new List<Clinic>());
        }
    }
}