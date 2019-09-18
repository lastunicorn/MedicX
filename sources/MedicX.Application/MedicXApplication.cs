using System;
using DustInTheWind.MedicX.Domain.Collections;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using EventBusModel;
using MedicDto = DustInTheWind.MedicX.Application.GetAllMedics.Medic;

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
            MedicDto medic = new MedicDto(e.Medic);
            eventAggregator["NewMedicAdded"].Raise(medic);
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
                foreach (Medic medic in medicXProject.Medics)
                {
                    medic.NameChanged += HandleMedicNameChanged;
                    medic.SpecializationsChanged += HandleMedicSpecializationsChanged;
                }

                medicXProject.Medics.Added += HandleMedicAdded;
            }
        }

        private void HandleMedicAdded(object sender, MedicAddedEventArgs e)
        {
            e.Medic.NameChanged += HandleMedicNameChanged;
            e.Medic.SpecializationsChanged += HandleMedicSpecializationsChanged;
        }

        private void HandleMedicNameChanged(object sender, EventArgs e)
        {
            if (sender is Medic medic)
            {
                MedicDto medicDto = new MedicDto(medic);
                eventAggregator["MedicNameChanged"].Raise(medicDto);
            }
        }

        private void HandleMedicSpecializationsChanged(object sender, EventArgs e)
        {
            if (sender is Medic medic)
            {
                MedicDto medicDto = new MedicDto(medic);
                eventAggregator["MedicSpecializationsChanged"].Raise(medicDto);
            }
        }

        private void HandleStatusChanged(object sender, EventArgs e)
        {
            eventAggregator["StatusChanged"].Raise(CurrentProject.Status);
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            switch (CurrentProject.CurrentItem)
            {
                case Medic medic:
                    eventAggregator["CurrentItemChanged"].Raise(new MedicDto(medic));
                    break;

                default:
                    eventAggregator["CurrentItemChanged"].Raise(CurrentProject.CurrentItem);
                    break;
            }

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