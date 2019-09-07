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
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.Wpf.Areas.MedicalEvents.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.Medics.ViewModels;

namespace DustInTheWind.MedicX.Wpf.Areas.Main.ViewModels
{
    internal class DetailsViewModel : ViewModelBase
    {
        private readonly MedicXProject medicXProject;
        private object item;
        private bool isNoItemPanelVisible;


        public object Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();

                IsNoItemPanelVisible = item == null;
            }
        }

        public bool IsNoItemPanelVisible
        {
            get => isNoItemPanelVisible;
            set
            {
                isNoItemPanelVisible = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel(MedicXProject medicXProject)
        {
            this.medicXProject = medicXProject ?? throw new ArgumentNullException(nameof(medicXProject));

            isNoItemPanelVisible = true;
            medicXProject.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            switch (medicXProject.CurrentItem)
            {
                case Consultation consultation:
                    Item = new ConsultationViewModel(consultation, medicXProject.Medics, medicXProject.Clinics);
                    break;

                case Investigation investigation:
                    Item = new InvestigationViewModel(investigation, medicXProject.Medics, medicXProject.Clinics);
                    break;

                case Medic medic:
                    Item = new MedicViewModel(medic);
                    break;

                default:
                    Item = medicXProject.CurrentItem;
                    break;
            }
        }
    }
}