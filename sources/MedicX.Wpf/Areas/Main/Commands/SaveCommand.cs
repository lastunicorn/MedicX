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
using System.Windows.Input;
using DustInTheWind.MedicX.Application.GetCurrentProjectStatus;
using DustInTheWind.MedicX.Application.SaveProject;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Wpf.Areas.Main.Commands
{
    internal class SaveCommand : ICommand
    {
        private readonly RequestBus requestBus;
        private ProjectStatusInfo projectStatusInfo;

        public event EventHandler CanExecuteChanged;

        public SaveCommand(RequestBus requestBus)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

            WatchCurrentProjectStatus();
        }

        private async void WatchCurrentProjectStatus()
        {
            GetCurrentProjectStatusRequest request = new GetCurrentProjectStatusRequest();
            projectStatusInfo = await requestBus.ProcessRequest<GetCurrentProjectStatusRequest, ProjectStatusInfo>(request);
            projectStatusInfo.StatusChanged += HandleStatusChanged;
        }

        private void HandleStatusChanged(object sender, EventArgs eventArgs)
        {
            System.Windows.Application.Current?.Dispatcher?.InvokeAsync(OnCanExecuteChanged);
        }

        public bool CanExecute(object parameter)
        {
            return projectStatusInfo?.Value != ProjectStatus.Saved;
        }

        public async void Execute(object parameter)
        {
            SaveProjectRequest request = new SaveProjectRequest();
            await requestBus.ProcessRequest(request);
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}