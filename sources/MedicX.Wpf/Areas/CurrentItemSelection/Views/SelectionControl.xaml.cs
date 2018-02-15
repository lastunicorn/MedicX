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

using System.Windows.Controls;
using DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.Views
{
    /// <summary>
    /// Interaction logic for SelectionControl.xaml
    /// </summary>
    internal partial class SelectionControl : UserControl
    {
        public SelectionControl()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl == null) return;

            TabItem selectedTabItem = tabControl.SelectedItem as TabItem;
            if (selectedTabItem == null) return;

            string tag = selectedTabItem.Tag as string;
            if (tag == null) return;

            SelectionViewModel viewModel = DataContext as SelectionViewModel;
            if (viewModel == null) return;

            switch (tag)
            {
                case "Medics":
                    viewModel.SelectedTab = Tab.Medics;
                    break;

                case "Clinics":
                    viewModel.SelectedTab = Tab.Clinics;
                    break;

                case "MedicalEvents":
                    viewModel.SelectedTab = Tab.Consultations;
                    break;

                default:
                    viewModel.SelectedTab = Tab.None;
                    break;
            }
        }
    }
}