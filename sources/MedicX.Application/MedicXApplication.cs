using System;
using DustInTheWind.MedicX.Domain.Collections;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using EventBusModel;

namespace DustInTheWind.MedicX.Application
{
    public class MedicXApplication
    {
        private readonly IUnitOfWorkBuilder unitOfWorkBuilder;
        private readonly EventAggregator eventAggregator;
        private string connectionString;
        private MedicXProject currentProject;

        public MedicXProject CurrentProject
        {
            get => currentProject;
            private set
            {
                if (currentProject != null)
                {
                    currentProject.CurrentItemChanged -= HandleCurrentItemChanged;
                    currentProject.StatusChanged -= HandleStatusChanged;

                    currentProject.Medics.Added -= HandleNewMedicAdded;
                    currentProject.Clinics.Added -= HandleNewClinicAdded;
                    currentProject.MedicalEvents.Added -= HandleNewMedicalEventAdded;
                }

                currentProject = value;

                if (currentProject != null)
                {
                    currentProject.CurrentItemChanged += HandleCurrentItemChanged;
                    currentProject.StatusChanged += HandleStatusChanged;

                    currentProject.Medics.Added += HandleNewMedicAdded;
                    currentProject.Clinics.Added += HandleNewClinicAdded;
                    currentProject.MedicalEvents.Added += HandleNewMedicalEventAdded;
                }
            }
        }

        private void HandleNewMedicAdded(object sender, MedicAddedEventArgs e)
        {
            eventAggregator["NewMedicAdded"].Raise(e.Medic);
        }

        private void HandleNewClinicAdded(object sender, ClinicAddedEventArgs e)
        {
            eventAggregator["NewClinicAdded"].Raise(e.Clinic);
        }

        private void HandleNewMedicalEventAdded(object sender, MedicalEventAddedEventArgs e)
        {
            eventAggregator["NewMedicalEventAdded"].Raise(e.MedicalEvent);
        }

        public MedicXApplication(IUnitOfWorkBuilder unitOfWorkBuilder, EventAggregator eventAggregator)
        {
            this.unitOfWorkBuilder = unitOfWorkBuilder ?? throw new ArgumentNullException(nameof(unitOfWorkBuilder));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public void LoadProject(string connectionString)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkBuilder.Build(connectionString))
            {
                MedicXProject medicXProject = new MedicXProject(unitOfWork);
                this.connectionString = connectionString;

                CurrentProject = medicXProject;
            }
        }

        private void HandleStatusChanged(object sender, EventArgs e)
        {
            eventAggregator["StatusChanged"].Raise(CurrentProject.Status);
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            eventAggregator["CurrentItemChanged"].Raise(CurrentProject.CurrentItem);
        }

        public void UnloadProject()
        {
            CurrentProject = null;
            connectionString = null;
        }

        public void SaveCurrentProject()
        {
            if (CurrentProject == null)
                return;

            using (IUnitOfWork unitOfWork = unitOfWorkBuilder.Build(connectionString))
            {
                CurrentProject.Save(unitOfWork);
            }
        }
    }
}