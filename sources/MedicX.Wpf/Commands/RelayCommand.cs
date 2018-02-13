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

namespace DustInTheWind.MedicX.Wpf.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Func<bool> canExecuteAction;
        private readonly Action executeAction;

        public RelayCommand(Action executeAction)
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
        }

        public RelayCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
            this.canExecuteAction = canExecuteAction ?? throw new ArgumentNullException(nameof(canExecuteAction));
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecuteAction?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            executeAction?.Invoke();
        }

        public void ReevaluateCanExecute()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}