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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemDetails.ViewModels
{
    internal class MedicViewModel : ViewModelBase
    {
        private readonly Medic medic;

        private PersonName name;
        private string comments;
        private bool isCommentsChanged;
        private ObservableCollection<string> specializations;

        public PersonName Name
        {
            get => name;
            set
            {
                if (Equals(value, name))
                    return;

                name = value;
                medic.Name = name;

                OnPropertyChanged();
            }
        }

        public string Comments
        {
            get => comments;
            set
            {
                comments = value;
                medic.Comments = comments;

                OnPropertyChanged();
            }
        }

        public bool IsCommentsChanged
        {
            get => isCommentsChanged;
            private set
            {
                isCommentsChanged = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Specializations
        {
            get => specializations;
            set
            {
                if ((specializations == null && value == null) || (specializations != null && value != null && specializations.SequenceEqual(value)))
                    return;

                specializations = value;
                medic.Specializations = specializations;

                OnPropertyChanged();
            }
        }

        public MedicViewModel(Medic medic)
        {
            this.medic = medic ?? throw new ArgumentNullException(nameof(medic));

            name = medic.Name;
            comments = medic.Comments;
            isCommentsChanged = medic.IsCommentsChanged;
            specializations = medic.Specializations;

            medic.Changed += HandleMedicChanged;
            medic.Name.Changed += HandleNameChanged;
        }

        private void HandleMedicChanged(object sender, EventArgs eventArgs)
        {
            IsCommentsChanged = medic.IsCommentsChanged;
        }

        private void HandleNameChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged(nameof(Name));
        }
    }
}