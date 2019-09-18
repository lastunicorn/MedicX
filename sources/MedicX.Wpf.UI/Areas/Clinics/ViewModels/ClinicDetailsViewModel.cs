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
using DustInTheWind.MedicX.Domain.Entities;
using EventBusModel;

namespace MedicX.Wpf.UI.Areas.Clinics.ViewModels
{
    internal class ClinicDetailsViewModel : ViewModelBase
    {
        private Clinic clinic;

        private string name;
        private string comments;
        private string program;
        private ObservableCollection<string> phones;
        private Address address;

        public string Name
        {
            get => name;
            set
            {
                if (Equals(value, name))
                    return;

                name = value;
                clinic.Name = name;

                OnPropertyChanged();
            }
        }

        public string Comments
        {
            get => comments;
            set
            {
                if(comments == value)
                    return;
                
                comments = value;
                clinic.Comments = comments;

                OnPropertyChanged();
            }
        }

        public string Program
        {
            get => program;
            set
            {
                if(program == value)
                    return;

                program = value;
                clinic.Program = program;

                OnPropertyChanged();
            }
        }

        public Address Address
        {
            get => address;
            set
            {
                if (address == value)
                    return;

                address = value;
                clinic.Address = address;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Phones
        {
            get => phones;
            set
            {
                if ((phones == null && value == null) || (phones != null && value != null && phones.SequenceEqual(value)))
                    return;

                phones = value;
                clinic.Phones = phones;

                OnPropertyChanged();
            }
        }

        public ClinicDetailsViewModel(Clinic clinic, EventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            this.clinic = clinic ?? throw new ArgumentNullException(nameof(clinic));

            UpdateDisplayedData();

            eventAggregator["CurrentItemChanged"].Subscribe(new Action<object>(HandleCurrentItemChanged));
        }

        private void HandleCurrentItemChanged(object currentItem)
        {
            if (currentItem is Clinic newClinic)
            {
                clinic = newClinic;
                UpdateDisplayedData();
            }
        }

        private void UpdateDisplayedData()
        {
            Name = clinic.Name;
            Comments = clinic.Comments;
            Program = clinic.Program;
            Address = clinic.Address;
            Phones = clinic.Phones;
        }
    }
}