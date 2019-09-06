using DustInTheWind.MedicX.Common;
using DustInTheWind.MedicX.Persistence;

namespace DustInTheWind.MedicX.Application
{
    public class MedicXApplication
    {
        private string connectionString;

        public MedicXProject CurrentProject { get; private set; }

        public void LoadProject(string connectionString)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(connectionString))
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

            using (UnitOfWork unitOfWork = new UnitOfWork(connectionString))
            {
                CurrentProject.Save(unitOfWork);
            }
        }
    }
}