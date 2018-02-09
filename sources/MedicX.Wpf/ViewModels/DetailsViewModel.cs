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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class DetailsViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;
        private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            Text = "<empty>";
            applicationState.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            switch (applicationState.CurrentItem)
            {
                case Medic medic:
                    Text = medic.Name;
                    break;

                case Clinic clinic:
                    Text = clinic.Name;
                    break;

                case Consultation consultation:
                    Text = string.Format("{0}{1}", consultation.Date, consultation.Medic.Name);
                    break;

                default:
                    Text = "<empty>";
                    break;
            }
        }
    }
}