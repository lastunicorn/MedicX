using DustInTheWind.MedicX.Business;

namespace DustInTheWind.MedicX.Application
{
    public class MedicXApplication
    {
        // keeps the state

        // manages  the project (load, save)
        // it is singleton

        public MedicXProject CurrentProject { get; private set; }

        public void LoadProject(string connectionString)
        {
            CurrentProject = new MedicXProject(connectionString);
        }

        public void UnloadProject()
        {
            CurrentProject = null;
        }
    }
}