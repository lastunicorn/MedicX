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
using System.Reflection;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title;
        private ObservableCollection<Medic> medics = new ObservableCollection<Medic>();
        private ObservableCollection<Clinic> clinics = new ObservableCollection<Clinic>();
        private ObservableCollection<Consultation> consultations = new ObservableCollection<Consultation>();

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Medic> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Clinic> Clinics
        {
            get { return clinics; }
            set
            {
                clinics = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Consultation> Consultations
        {
            get { return consultations; }
            set
            {
                consultations = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            UpdateWindowTitle();
            UpdateMedics();
            UpdateClinics();
            UpdateConsultations();
        }

        private void UpdateWindowTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;

            Title = "MedicX " + version.ToString(3);
        }

        private void UpdateMedics()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                IMedicRepository medicRepository = unitOfWork.MedicRepository;

                List<Medic> medicsFromRepository = medicRepository.GetAll();

                foreach (Medic medic in medicsFromRepository)
                    medics.Add(medic);
            }
        }

        private void UpdateClinics()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

                List<Clinic> clinicsFromRepository = clinicRepository.GetAll();

                foreach (Clinic clinic in clinicsFromRepository)
                    clinics.Add(clinic);
            }
        }

        private void UpdateConsultations()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                IConsultationsRepository consultationsRepository = unitOfWork.ConsultationsRepository;

                List<Consultation> consultationFromRepository = consultationsRepository.GetAll();

                foreach (Consultation consultation in consultationFromRepository)
                    consultations.Add(consultation);
            }
        }
    }
}