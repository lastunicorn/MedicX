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
using DustInTheWind.MedicX.Business;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf.Areas.CurrentItemDetails.ViewModels
{
    internal class InvestigationViewModel : ViewModelBase
    {
        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public Investigation Investigation { get; }

        public List<Medic> Medics { get; }

        public List<Clinic> Clinics { get; }

        public InvestigationViewModel(Investigation investigation, MedicsCollection medics, ClinicsCollection clinics)
        {
            if (investigation == null) throw new ArgumentNullException(nameof(investigation));
            if (medics == null) throw new ArgumentNullException(nameof(medics));
            if (clinics == null) throw new ArgumentNullException(nameof(clinics));

            Investigation = investigation;

            Investigation.DateChanged += HandleInvestigationDateChanged;
            Investigation.SentByChanged += HandleInvestigationSentByChanged;

            Medics = medics
                .OrderBy(x => x.Name)
                .ToList();

            Clinics = clinics
                .OrderBy(x => x.Name)
                .ToList();

            UpdateTitle();
        }

        private void HandleInvestigationDateChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void HandleInvestigationSentByChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            Title = string.Format("{0:yyyy MM dd} - Sent by {1} - (investigation)", Investigation.Date, Investigation.SentBy.Name);
        }
    }
}