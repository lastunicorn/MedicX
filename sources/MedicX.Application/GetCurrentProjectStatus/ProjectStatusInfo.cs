using System;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Application.GetCurrentProjectStatus
{
    public class ProjectStatusInfo
    {
        private readonly Project project;

        public event EventHandler StatusChanged;

        public ProjectStatus Value => project.Status;

        public ProjectStatusInfo(Project project)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));

            this.project.StatusChanged += HandleProjectStatusChanged;
        }

        private void HandleProjectStatusChanged(object sender, EventArgs e)
        {
            OnStatusChanged();
        }

        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}