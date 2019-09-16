using System;
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.UpdateConsultationClinic
{
    internal class UpdateConsultationSetClinicRequestHandler : IRequestHandler<UpdateConsultationSetClinicRequest>
    {
        private readonly MedicXApplication application;

        public UpdateConsultationSetClinicRequestHandler(MedicXApplication application)
        {
            this.application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public Task Handle(UpdateConsultationSetClinicRequest request)
        {
            return Task.Run(() =>
            {
                Consultation consultation = application.CurrentProject?.MedicalEvents
                    .OfType<Consultation>()
                    .FirstOrDefault(x => x.Id == request.ConsultationId);

                if (consultation == null)
                    return;

                if (request.ClinicId == Guid.Empty)
                {
                    consultation.Clinic = null;
                    return;
                }

                Clinic clinic = application.CurrentProject?.Clinics
                    .FirstOrDefault(x => x.Id == request.ClinicId);

                if (clinic == null)
                    return;

                consultation.Clinic = clinic;
            });
        }
    }
}