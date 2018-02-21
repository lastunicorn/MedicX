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

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Consultation : MedicalEvent
    {
        private Medic medic;

        public Medic Medic
        {
            get => medic;
            set
            {
                medic = value;
                OnMedicChanged();
            }
        }

        public List<Prescription> Prescriptions { get; set; }

        public event EventHandler MedicChanged;

        public void CopyFrom(Consultation consultation)
        {
            base.CopyFrom(consultation);

            Medic = consultation.Medic;
            Prescriptions = consultation.Prescriptions.ToList();
        }

        public override void CopyFrom(MedicalEvent medicalEvent)
        {
            if (medicalEvent is Consultation consultation)
                CopyFrom(consultation);
            else
                base.CopyFrom(medicalEvent);
        }

        protected virtual void OnMedicChanged()
        {
            MedicChanged?.Invoke(this, EventArgs.Empty);
        }

        public override bool Contains(string text)
        {
            return base.Contains(text) ||
                   (Medic != null && Medic.Contains(text)) ||
                   (Prescriptions != null && Prescriptions.Any(x => x != null && x.Contains(text)));
        }
    }
}