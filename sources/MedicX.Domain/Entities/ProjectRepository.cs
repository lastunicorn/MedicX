namespace DustInTheWind.MedicX.Domain.Entities
{
    public class ProjectRepository
    {
        public Project CurrentProject { get; set; }

        public void GetAll()
        {
        }

        public void Add(Project project)
        {
        }

        public void Remove(Project project)
        {
        }

        public void SetCurrent(Project project)
        {
            CurrentProject = project;
        }
    }
}