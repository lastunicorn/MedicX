using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.GetAllMedics
{
    internal class GetAllMedicsRequestHandler : IRequestHandler<GetAllMedicsRequest, List<Medic>>
    {
        private readonly MedicXApplication medicXApplication;

        public GetAllMedicsRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task<List<Medic>> Handle(GetAllMedicsRequest request)
        {
            List<Medic> medics = medicXApplication.CurrentProject?.Medics.ToList() ?? new List<Medic>();
            return Task.FromResult(medics);
        }
    }
}