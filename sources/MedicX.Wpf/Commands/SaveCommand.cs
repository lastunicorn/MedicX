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
using System.Windows.Input;
using DustInTheWind.MedicX.Business;

namespace DustInTheWind.MedicX.Wpf.Commands
{
    internal class SaveCommand : ICommand
    {
        private readonly MedicXProject medicXProject;

        public event EventHandler CanExecuteChanged;

        public SaveCommand(MedicXProject medicXProject)
        {
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            this.medicXProject.StatusChanged += HandleStatusChanged;
        }

        private void HandleStatusChanged(object sender, EventArgs eventArgs)
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return medicXProject.Status != ProjectStatus.Saved;
        }

        public void Execute(object parameter)
        {
            medicXProject.Save();
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}