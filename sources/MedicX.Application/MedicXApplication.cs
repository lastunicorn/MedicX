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
                }

                currentProject = value;

                if (currentProject != null)
                {
                    currentProject.CurrentItemChanged += HandleCurrentItemChanged;
                }
            }
        }

        public MedicXApplication(IUnitOfWorkBuilder unitOfWorkBuilder, EventAggregator eventAggregator)
        {
            this.unitOfWorkBuilder = unitOfWorkBuilder ?? throw new ArgumentNullException(nameof(unitOfWorkBuilder));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public void LoadProject(string connectionString)
        {
            CurrentProject = new Project(unitOfWorkBuilder, connectionString);
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
        }
    }
}