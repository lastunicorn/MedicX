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
using DustInTheWind.MedicX.Application.GetCurrentProject;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using EventBusModel;
using MedicX.Wpf.UI.Areas.Main.Commands;

namespace MedicX.Wpf.UI.Areas.Main.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly RequestBus requestBus;
        private MainWindowTitle title;

        public MainWindowTitle Title
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

        public MainViewModel(RequestBus requestBus, EventAggregator eventAggregator)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            //SelectionViewModel = new SelectionViewModel(requestBus, eventAggregator, medicXProject);
            DetailsViewModel = new DetailsViewModel(requestBus, eventAggregator);

            SaveCommand = new SaveCommand(requestBus);
            ExitCommand = new ExitCommand(requestBus);

            eventAggregator["StatusChanged"].Subscribe(new Action<ProjectStatus>(HandleProjectStatusChanged));
            Title = new MainWindowTitle();
            UpdateWindowTitle();
        }

        private void HandleProjectStatusChanged(ProjectStatus newProjectStatus)
        {
            Title = new MainWindowTitle(newProjectStatus);
        }

        private void UpdateWindowTitle()
        {
            GetCurrentProjectRequest request = new GetCurrentProjectRequest();

            requestBus.ProcessRequest<GetCurrentProjectRequest, MedicXProject>(request)
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                        Title = new MainWindowTitle(t.Result.Status);
                    else
                    {
                        // todo: Display an error message
                    }
                });
        }
    }
}