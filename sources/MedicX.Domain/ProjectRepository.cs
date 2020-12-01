using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Domain
{
    public class ProjectRepository
    {
        private readonly List<Project> projects = new List<Project>();

        public Project ActiveProject { get; private set; }

        public IEnumerable<Project> GetAll()
        {
            return projects.ToList();
        }

        public void Add(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            bool alreadyExists = projects.Contains(project);

            if (alreadyExists)
                return;

            projects.Add(project);
        }

        public void Remove(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));

            if (project == ActiveProject)
                ActiveProject = null;

            projects.Remove(project);
        }

        public void SetActive(Project project)
        {
            bool exists = projects.Contains(project);

            if (!exists)
                throw new ArgumentException("Project is not part of the repository.", nameof(project));

            ActiveProject = project;
        }

        public void RemoveActive()
        {
            if (ActiveProject == null)
                return;

            Project project = ActiveProject;
            ActiveProject = null;

            projects.Remove(project);
        }

        public void RunWithUnitOfWork(Action<IUnitOfWork> action)
        {
            Project activeProject = ActiveProject;

            if (activeProject == null)
                throw new Exception("There is no active project.");

            using (IUnitOfWork unitOfWork = activeProject.CreateUnitOfWork())
                action(unitOfWork);
        }
    }
}