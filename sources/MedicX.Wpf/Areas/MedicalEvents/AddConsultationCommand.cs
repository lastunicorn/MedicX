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
using System.Collections.ObjectModel;
using System.Windows.Input;
using DustInTheWind.MedicX.Business;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.Areas.MedicalEvents
{
    internal class AddConsultationCommand : ICommand
    {
        private readonly MedicXProject medicXProject;

        public event EventHandler CanExecuteChanged;

        public AddConsultationCommand(MedicXProject medicXProject)
        {
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Consultation consultation = new Consultation
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Today,
                Labels = new ObservableCollection<string>()
            };

            medicXProject.MedicalEvents.Add(consultation);
            medicXProject.CurrentItem = consultation;
        }
    }
}