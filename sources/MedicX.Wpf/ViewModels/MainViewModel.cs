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
using System.Reflection;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public SelectionViewModel SelectionViewModel { get; }

        public DetailsViewModel DetailsViewModel { get; }

        public MainViewModel()
        {
            UpdateWindowTitle();

            ApplicationState applicationState = new ApplicationState();

            SelectionViewModel = new SelectionViewModel(applicationState);
            DetailsViewModel = new DetailsViewModel(applicationState);
        }

        private void UpdateWindowTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;

            Title = "MedicX " + version.ToString(3);
        }
    }
}