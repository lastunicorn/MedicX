using System;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Application
{
    public class MedicXApplication
    {
        private readonly IUnitOfWorkBuilder unitOfWorkBuilder;
        private string connectionString;

        public MedicXProject CurrentProject { get; private set; }

        public MedicXApplication(IUnitOfWorkBuilder unitOfWorkBuilder)
        {
            this.unitOfWorkBuilder = unitOfWorkBuilder ?? throw new ArgumentNullException(nameof(unitOfWorkBuilder));
        }

        public void LoadProject(string connectionString)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkBuilder.Build(connectionString))
            {
                MedicXProject medicXProject = new MedicXProject();
                medicXProject.LoadData(unitOfWork);
                this.connectionString = connectionString;

                CurrentProject = medicXProject;
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