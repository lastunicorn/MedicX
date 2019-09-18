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
using System.Linq;
using DustInTheWind.MedicX.Application.GetAllMedics;

namespace DustInTheWind.MedicX.Wpf.UI.Areas.Medics.ViewModels
{
    internal class MedicItemViewModel : ViewModelBase
    {
        private string name;
        private List<string> specializations;

        public Medic Medic { get; set; }

        public string Name
        {
            get => name;
            private set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public List<string> Specializations
        {
            get => specializations;
            private set
            {
                specializations = value;
                OnPropertyChanged();
            }
        }

        public MedicItemViewModel(Medic medic)
        {
            Medic = medic ?? throw new ArgumentNullException(nameof(medic));

            UpdateDisplayedName();
            UpdateDisplayedSpecializations();

            //medic.NameChanged += HandleMedicNameChanged;
            //medic.SpecializationsChanged += HandleSpecializationsChanged;
        }

        //private void HandleMedicNameChanged(object sender, EventArgs e)
        //{
        //    UpdateDisplayedName();
        //}

        //private void HandleSpecializationsChanged(object sender, EventArgs e)
        //{
        //    UpdateDisplayedSpecializations();
        //}

        private void UpdateDisplayedName()
        {
            string personName = Medic.Name?.ToString();

            Name = string.IsNullOrEmpty(personName)
                ? "<no name>"
                : personName;
        }

        private void UpdateDisplayedSpecializations()
        {
            Specializations = Medic.Specializations.ToList();
        }

        public void UpdateFrom(Medic medic)
        {
            Medic = medic ?? throw new ArgumentNullException(nameof(medic));

            UpdateDisplayedName();
            UpdateDisplayedSpecializations();
        }
    }
}