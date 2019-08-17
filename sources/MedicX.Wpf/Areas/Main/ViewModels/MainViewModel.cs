// MedicX
// Copyright (C) 2017-2018 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Diagnostics;
using System.Reflection;
using DustInTheWind.MedicX.Application.GetCurrentProject;
using DustInTheWind.MedicX.Business;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.Areas.CurrentItemDetails.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels;
using DustInTheWind.MedicX.Wpf.Commands;

namespace DustInTheWind.MedicX.Wpf.Areas.Main.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title;
        private readonly MedicXProject medicXProject;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public SelectionViewModel SelectionViewModel { get; }

        public DetailsViewModel DetailsViewModel { get; }

        public SaveCommand SaveCommand { get; }
        public ExitCommand ExitCommand { get; }

        public MainViewModel(RequestBus requestBus)
        {
            medicXProject = AsyncUtil.RunSync(() =>
            {
                GetCurrentProjectRequest request = new GetCurrentProjectRequest();
                return requestBus.ProcessRequest<GetCurrentProjectRequest, MedicXProject>(request);
            });

            SelectionViewModel = new SelectionViewModel(medicXProject);
            DetailsViewModel = new DetailsViewModel(medicXProject);

            SaveCommand = new SaveCommand(requestBus);
            ExitCommand = new ExitCommand(requestBus);

            medicXProject.StatusChanged += HandleProjectStatusChanged;
            UpdateWindowTitle();
        }

        private void HandleProjectStatusChanged(object sender, EventArgs e)
        {
            UpdateWindowTitle();
        }

        private void UpdateWindowTitle()
        {
            string name = BuildName();

            Title = medicXProject.Status == ProjectStatus.Saved
                ? name
                : name + " *";
        }

        private static string BuildName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyInformationalVersionAttribute attribute = assembly.GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;

            string version;
            if (attribute == null)
            {
                AssemblyName assemblyName = assembly.GetName();

                version = assemblyName.Version.Build == 0
                    ? assemblyName.Version.ToString(2)
                    : assemblyName.Version.ToString(3);
            }
            else
            {
                version = attribute.InformationalVersion;
            }

            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            string productName = fileVersionInfo.ProductName;

            return $"{productName} {version}";
        }
    }
}