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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Wpf
{
    internal class ApplicationState
    {
        private object currentItem;

        public ObservableCollection<Medic> Medics { get; } = new ObservableCollection<Medic>();

        public ObservableCollection<Clinic> Clinics { get; } = new ObservableCollection<Clinic>();

        public ObservableCollection<MedicalEvent> MedicalEvents { get; } = new ObservableCollection<MedicalEvent>();

        public object CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                OnCurrentItemChanged();
            }
        }

        public event EventHandler CurrentItemChanged;

        public ApplicationState()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                IMedicRepository medicRepository = unitOfWork.MedicRepository;

                List<Medic> medicsFromRepository = medicRepository.GetAll();

                foreach (Medic medic in medicsFromRepository)
                    Medics.Add(medic);

                IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

                List<Clinic> clinicsFromRepository = clinicRepository.GetAll();

                foreach (Clinic clinic in clinicsFromRepository)
                    Clinics.Add(clinic);

                IConsultationRepository consultationRepository = unitOfWork.ConsultationRepository;
                IInvestigationRepository investigationsRepository = unitOfWork.InvestigationRepository;

                List<Consultation> consultationsFromRepository = consultationRepository.GetAll();
                List<Investigation> investigationsFromRepository = investigationsRepository.GetAll();

                List<MedicalEvent> medicalEvents = consultationsFromRepository
                    .Cast<MedicalEvent>()
                    .Concat(investigationsFromRepository)
                    .OrderByDescending(x=>x.Date)
                    .ToList();

                foreach (MedicalEvent medicalEvent in medicalEvents)
                    MedicalEvents.Add(medicalEvent);
            }
        }

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}