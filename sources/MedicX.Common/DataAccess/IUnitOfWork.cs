namespace DustInTheWind.MedicX.Common.DataAccess
{
    public interface IUnitOfWork
    {
        IMedicRepository MedicRepository { get; }
        IConsultationRepository ConsultationRepository { get; }
        IInvestigationRepository InvestigationRepository { get; }
        IClinicRepository ClinicRepository { get; }
        void Save();
    }
}