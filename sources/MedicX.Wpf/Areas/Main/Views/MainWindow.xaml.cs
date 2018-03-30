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

using System.Windows;
using DustInTheWind.MedicX.Wpf.Areas.Calendar.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.Calendar.Views;
using DustInTheWind.MedicX.Wpf.Areas.Main.ViewModels;

namespace DustInTheWind.MedicX.Wpf.Areas.Main.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MedicXProject medicXProject = new MedicXProject();
            DataContext = new MainViewModel(medicXProject);

            //CalendarWindow calendarWindow = new CalendarWindow
            //{
            //    DataContext = new CalendarViewModel(medicXProject)
            //};
            //calendarWindow.Show();
        }
    }
}
