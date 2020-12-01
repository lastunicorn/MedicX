using System;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.GuiApplication.UpdateConsultationClinic
{
    internal class UpdateConsultationSetClinicRequestHandler : IRequestHandler<UpdateConsultationSetClinicRequest>
    {
        private readonly ProjectRepository projectRepository;

        public UpdateConsultationSetClinicRequestHandler(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public Task Handle(UpdateConsultationSetClinicRequest request)
        {
            return Task.Run(() =>
            {
                Project currentProject = projectRepository.ActiveProject;

                if (currentProject == null)
                    throw new NoProjectException();

                using (IUnitOfWork unitOfWork = currentProject.CreateUnitOfWork())
                {
                    Consultation consultation = RetrieveConsultation(unitOfWork, request.ConsultationId);
                    consultation.Clinic = RetrieveClinic(unitOfWork, request.ClinicId);
                }
            });
        }

        private static Consultation RetrieveConsultation(IUnitOfWork unitOfWork, Guid consultationId)
        {
            Consultation consultation = unitOfWork.ConsultationRepository.GetById(consultationId);

            if (consultation == null)
                throw new Exception($"Consultation with id {consultationId} was not found.");

            return consultation;
        }

        private static Clinic RetrieveClinic(IUnitOfWork unitOfWork, Guid clinicId)
        {
            if (clinicId == Guid.Empty)
                return null;

            Clinic clinic = unitOfWork.ClinicRepository.GetById(clinicId);

            if (clinic == null)
                throw new Exception($"Clinic with id {clinicId} not found.");

            return clinic;
        }
    }
}