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
using System.Linq;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.Calendar.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {
        private Project project;

        private DateTime januaryDate;
        private DateTime februaryDate;
        private DateTime marchDate;
        private ObservableCollection<DateTime> januarySelectedDates;

        public CalendarViewModel(Project project)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));

            const int year = 1980;

            januaryDate = new DateTime(year, 01, 01);
            februaryDate = new DateTime(year, 02, 01);
            marchDate = new DateTime(year, 03, 01);

            //IEnumerable<DateTime> dates = project.MedicalEvents
            //    .Where(x => x.Date >= new DateTime(year, 01, 01) && x.Date <= new DateTime(year, 12, 31))
            //    .Select(x => x.Date);

            IEnumerable<DateTime> dates = Enumerable.Empty<DateTime>();

            januarySelectedDates = new ObservableCollection<DateTime>(dates);
        }

        public DateTime JanuaryDate
        {
            get => januaryDate;
            set
            {
                januaryDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime FebruaryDate
        {
            get => februaryDate;
            set
            {
                februaryDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime MarchDate
        {
            get => marchDate;
            set
            {
                marchDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DateTime> JanuarySelectedDates
        {
            get => januarySelectedDates;
            set
            {
                januarySelectedDates = value;
                OnPropertyChanged();
            }
        }
    }
}