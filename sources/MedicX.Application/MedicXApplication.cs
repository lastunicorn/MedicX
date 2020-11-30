using System;
using DustInTheWind.MedicX.Application.GetAllMedics;
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
        private Project currentProject;

        public Project CurrentProject
        {
            get => currentProject;
            private set
            {
                if (currentProject != null)
                {
                    currentProject.CurrentItemChanged -= HandleCurrentItemChanged;
                    //currentProject.StatusChanged -= HandleStatusChanged;

                    //currentProject.Medics.Added -= HandleNewMedicAdded;
                    //currentProject.Clinics.Added -= HandleNewClinicAdded;
                    //currentProject.MedicalEvents.Added -= HandleNewMedicalEventAdded;
                }

                currentProject = value;

                if (currentProject != null)
                {
                    currentProject.CurrentItemChanged += HandleCurrentItemChanged;
                    //currentProject.StatusChanged += HandleStatusChanged;

                    //currentProject.Medics.Added += HandleNewMedicAdded;
                    //currentProject.Clinics.Added += HandleNewClinicAdded;
                    //currentProject.MedicalEvents.Added += HandleNewMedicalEventAdded;
                }
            }
        }

        //private void HandleNewMedicAdded(object sender, MedicAddedEventArgs e)
        //{
        //    MedicDto medic = new MedicDto(e.Medic);
        //    eventAggregator["NewMedicAdded"].Raise(medic);
        //}

        //private void HandleNewClinicAdded(object sender, ClinicAddedEventArgs e)
        //{
        //    eventAggregator["NewClinicAdded"].Raise(e.Clinic);
        //}

        //private void HandleNewMedicalEventAdded(object sender, MedicalEventAddedEventArgs e)
        //{
        //    eventAggregator["NewMedicalEventAdded"].Raise(e.MedicalEvent);
        //}

        public MedicXApplication(IUnitOfWorkBuilder unitOfWorkBuilder, EventAggregator eventAggregator)
        {
            this.unitOfWorkBuilder = unitOfWorkBuilder ?? throw new ArgumentNullException(nameof(unitOfWorkBuilder));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public void LoadProject(string connectionString)
        {
            CurrentProject = new Project(unitOfWorkBuilder, connectionString);
        }

        //public void LoadProject(string connectionString)
        //{
        //    using (IUnitOfWork unitOfWork = unitOfWorkBuilder.Build(connectionString))
        //    {
        //        Project project = new Project(unitOfWork);
        //        this.connectionString = connectionString;

        //        CurrentProject = project;
        //        foreach (Medic medic in project.Medics)
        //        {
        //            medic.NameChanged += HandleMedicNameChanged;
        //            medic.SpecializationsChanged += HandleMedicSpecializationsChanged;
        //        }

        //        project.Medics.Added += HandleMedicAdded;
        //    }
        //}

        //private void HandleMedicAdded(object sender, MedicAddedEventArgs e)
        //{
        //    e.Medic.NameChanged += HandleMedicNameChanged;
        //    e.Medic.SpecializationsChanged += HandleMedicSpecializationsChanged;
        //}

        //private void HandleMedicNameChanged(object sender, EventArgs e)
        //{
        //    if (sender is Medic medic)
        //    {
        //        MedicDto medicDto = new MedicDto(medic);
        //        eventAggregator["MedicNameChanged"].Raise(medicDto);
        //    }
        //}

        //private void HandleMedicSpecializationsChanged(object sender, EventArgs e)
        //{
        //    if (sender is Medic medic)
        //    {
        //        MedicDto medicDto = new MedicDto(medic);
        //        eventAggregator["MedicSpecializationsChanged"].Raise(medicDto);
        //    }
        //}

        //private void HandleStatusChanged(object sender, EventArgs e)
        //{
        //    eventAggregator["StatusChanged"].Raise(CurrentProject.Status);
        //}

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
        }

        //public void SaveCurrentProject()
        //{
        //    if (CurrentProject == null)
        //        return;

        //    using (IUnitOfWork unitOfWork = unitOfWorkBuilder.Build(connectionString))
        //    {
        //        CurrentProject.Save(unitOfWork);
        //    }
        //}
    }
}