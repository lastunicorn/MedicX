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
using DustInTheWind.MedicX.Application.ExitApplication;
using DustInTheWind.MedicX.RequestBusModel;

namespace MedicX.Wpf.UI.Areas.Main.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly RequestBus requestBus;

        public event EventHandler CanExecuteChanged;

        public ExitCommand(RequestBus requestBus)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            bool allowToContinue = AsyncUtil.RunSync(() =>
            {
                ExitApplicationRequest request = new ExitApplicationRequest();
                return requestBus.ProcessRequest<ExitApplicationRequest, bool>(request);
            });

            if (allowToContinue)
                System.Windows.Application.Current.Shutdown();
        }
    }
}