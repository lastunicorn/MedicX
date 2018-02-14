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

        public DetailsViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

            isNoItemPanelVisible = true;
            applicationState.CurrentItemChanged += HandleCurrentItemChanged;
        }

        private void HandleCurrentItemChanged(object sender, EventArgs e)
        {
            switch (applicationState.CurrentItem)
            {
                case Consultation _:
                    Item = new ConsultationViewModel(applicationState.CurrentItem as Consultation, applicationState.Medics);
                    break;
                    
                case Investigation _:
                    Item = new InvestigationViewModel(applicationState.CurrentItem as Investigation, applicationState.Medics);
                    break;

                default:
                    Item = applicationState.CurrentItem;
                    break;
            }
        }
    }
}