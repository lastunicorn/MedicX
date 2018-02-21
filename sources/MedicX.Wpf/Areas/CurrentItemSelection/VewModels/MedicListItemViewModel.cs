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

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemSelection.VewModels
{
    internal class MedicListItemViewModel : ListItemViewModel<Medic>
    {
        public MedicListItemViewModel(Medic medic)
        {
            Value = medic ?? throw new ArgumentNullException(nameof(medic));

            UpdateText();
            medic.NameChanged += HandleMedicNameChanged;
        }

        private void HandleMedicNameChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            string personName = Value.Name?.ToString();

            Text = string.IsNullOrEmpty(personName)
                ? "<no name>"
                : personName;
        }
    }
}