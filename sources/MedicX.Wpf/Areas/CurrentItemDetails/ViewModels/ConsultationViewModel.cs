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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemDetails.ViewModels
{
    internal class ConsultationViewModel : ViewModelBase
    {
        public Consultation Consultation { get; }

        public List<Medic> Medics { get; }

        public List<Clinic> Clinics { get; }

        public ConsultationViewModel(Consultation consultation, MedicsCollection medics, ClinicsCollection clinics)
        {
            if (consultation == null) throw new ArgumentNullException(nameof(consultation));
            if (medics == null) throw new ArgumentNullException(nameof(medics));
            if (clinics == null) throw new ArgumentNullException(nameof(clinics));

            Consultation = consultation;

            Medics = medics
                .OrderBy(x => x.Name)
                .ToList();

            Clinics = clinics
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}