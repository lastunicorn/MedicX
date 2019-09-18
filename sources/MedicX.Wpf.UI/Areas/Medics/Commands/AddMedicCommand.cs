﻿// MedicX
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
using System.Threading.Tasks;
using System.Windows.Input;
using DustInTheWind.MedicX.Application.AddNewMedic;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.Medics.Commands
{
    internal class AddMedicCommand : ICommand
    {
        private readonly RequestBus requestBus;

        public event EventHandler CanExecuteChanged;

        public AddMedicCommand(RequestBus requestBus)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddNewMedicRequest request = new AddNewMedicRequest();
            requestBus.ProcessRequest(request)
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        // todo: display error message
                    }
                }, TaskContinuationOptions.ExecuteSynchronously)
                .ConfigureAwait(true);
        }
    }
}