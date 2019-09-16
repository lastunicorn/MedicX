using System;

namespace DustInTheWind.MedicX.Application.UpdateConsultationClinic
{
    public class UpdateConsultationSetClinicRequest
    {
        public Guid ConsultationId { get; set; }
        public Guid ClinicId { get; set; }
    }
}