using System;

namespace DustInTheWind.MedicX.GuiApplication.UpdateConsultationClinic
{
    public class UpdateConsultationSetClinicRequest
    {
        public Guid ConsultationId { get; set; }
        public Guid ClinicId { get; set; }
    }
}