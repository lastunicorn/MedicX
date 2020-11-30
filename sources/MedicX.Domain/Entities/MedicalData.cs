using System;
using DustInTheWind.MedicX.Domain.Collections;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class MedicalData
    {
        private DataStatus status = DataStatus.New;

        public MedicsCollection Medics { get; } = new MedicsCollection();

        public ClinicsCollection Clinics { get; } = new ClinicsCollection();

        public MedicalEventCollection MedicalEvents { get; } = new MedicalEventCollection();

        public DataStatus Status
        {
            get => status;
            private set
            {
                status = value;
                OnStatusChanged();
            }
        }

        public event EventHandler StatusChanged;

        public MedicalData()
        {
            Medics.Changed += HandleMedicsCollectionChanged;
            Clinics.Changed += HandleClinicsCollectionChanged;
            MedicalEvents.Changed += HandleMedicalEventsCollectionChanged;
        }

        private void HandleMedicsCollectionChanged(object sender, EventArgs e)
        {
            Status = DataStatus.Modified;
        }

        private void HandleClinicsCollectionChanged(object sender, EventArgs e)
        {
            Status = DataStatus.Modified;
        }

        private void HandleMedicalEventsCollectionChanged(object sender, EventArgs e)
        {
            Status = DataStatus.Modified;
        }

        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}