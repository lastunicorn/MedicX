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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace DustInTheWind.MedicX.Wpf.Views
{
    internal class StringListViewModel : ViewModelBase
    {
        private int currentIndex = -1;

        public ObservableCollection<string> Values { get; }

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                currentIndex = value;
                OnPropertyChanged();

                RemoveCommand.ReevaluateCanExecute();
            }
        }

        public StringListViewModel()
        : this(null)
        {
        }

        public StringListViewModel(IEnumerable<string> values)
        {
            Values = new ObservableCollection<string>(values);

            AddCommand = new RelayCommand(ExecuteAddCommand);
            RemoveCommand = new RelayCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand);
        }

        private void ExecuteAddCommand()
        {
            Values.Add(string.Empty);
        }

        private bool CanExecuteRemoveCommand()
        {
            return CurrentIndex >= 0 && CurrentIndex < Values.Count;
        }

        private void ExecuteRemoveCommand()
        {
            Values.RemoveAt(CurrentIndex);
        }
    }

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

    internal class StringListViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new StringListViewModel(value as List<string>);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<string>();
        }
    }
}