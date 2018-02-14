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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class ConsultationViewModel : ViewModelBase
    {
        public Consultation Consultation { get; }

        public ObservableCollection<Medic> Medics { get; }

        public ConsultationViewModel(Consultation consultation, ObservableCollection<Medic> medics)
        {
            Consultation = consultation ?? throw new ArgumentNullException(nameof(consultation));
            Medics = medics ?? throw new ArgumentNullException(nameof(medics));
        }
    }
}