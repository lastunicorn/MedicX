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
using DustInTheWind.MedicX.Domain.DataAccess;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class Project
    {
        private readonly IUnitOfWorkBuilder unitOfWorkBuilder;

        private readonly string connectionString;
        //private ProjectStatus status = ProjectStatus.New;

        private object currentItem;

        public object CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                OnCurrentItemChanged();
            }
        }

        //public ProjectStatus Status
        //{
        //    get => status;
        //    private set
        //    {
        //        status = value;
        //        OnStatusChanged();
        //    }
        //}

        public event EventHandler CurrentItemChanged;
        //public event EventHandler StatusChanged;

        //public Project()
        //{
        //    Medics.Changed += HandleMedicsCollectionChanged;
        //    Clinics.Changed += HandleClinicsCollectionChanged;
        //    MedicalEvents.Changed += HandleMedicalEventsCollectionChanged;
        //}

        //public Project(IUnitOfWork unitOfWork)
        //    : this()
        //{
        //    LoadFrom(unitOfWork);
        //}

        public Project(IUnitOfWorkBuilder unitOfWorkBuilder, string connectionString)
        {
            this.unitOfWorkBuilder = unitOfWorkBuilder ?? throw new ArgumentNullException(nameof(unitOfWorkBuilder));
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return unitOfWorkBuilder.Build(connectionString);
        }

        //private void HandleMedicsCollectionChanged(object sender, EventArgs e)
        //{
        //    Status = ProjectStatus.Modified;
        //}

        //private void HandleClinicsCollectionChanged(object sender, EventArgs e)
        //{
        //    Status = ProjectStatus.Modified;
        //}

        //private void HandleMedicalEventsCollectionChanged(object sender, EventArgs e)
        //{
        //    Status = ProjectStatus.Modified;
        //}

        //private void LoadFrom(IUnitOfWork unitOfWork)
        //{
        //    IMedicRepository medicRepository = unitOfWork.MedicRepository;

        //    List<Medic> medicsFromRepository = medicRepository.GetAll();

        //    foreach (Medic medic in medicsFromRepository)
        //        Medics.Add(medic);

        //    IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

        //    List<Clinic> clinicsFromRepository = clinicRepository.GetAll();

        //    foreach (Clinic clinic in clinicsFromRepository)
        //        Clinics.Add(clinic);

        //    IConsultationRepository consultationRepository = unitOfWork.ConsultationRepository;
        //    IInvestigationRepository investigationsRepository = unitOfWork.InvestigationRepository;

        //    List<Consultation> consultationsFromRepository = consultationRepository.GetAll();
        //    List<Investigation> investigationsFromRepository = investigationsRepository.GetAll();

        //    IEnumerable<MedicalEvent> medicalEvents = investigationsFromRepository
        //        .Cast<MedicalEvent>()
        //        .Concat(consultationsFromRepository);

        //    foreach (MedicalEvent medicalEvent in medicalEvents)
        //        MedicalEvents.Add(medicalEvent);

        //    Status = ProjectStatus.Saved;
        //}

        //public void Save(IUnitOfWork unitOfWork)
        //{
        //    foreach (Medic medic in Medics)
        //        unitOfWork.MedicRepository.AddOrUpdate(medic);

        //    foreach (Clinic clinic in Clinics)
        //        unitOfWork.ClinicRepository.AddOrUpdate(clinic);

        //    foreach (MedicalEvent medicalEvent in MedicalEvents)
        //    {
        //        switch (medicalEvent)
        //        {
        //            case Consultation consultation:
        //                unitOfWork.ConsultationRepository.AddOrUpdate(consultation);
        //                break;

        //            case Investigation investigation:
        //                unitOfWork.InvestigationRepository.AddOrUpdate(investigation);
        //                break;
        //        }
        //    }

        //    unitOfWork.Save();

        //    Status = ProjectStatus.Saved;
        //}

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke(this, EventArgs.Empty);
        }

        //protected virtual void OnStatusChanged()
        //{
        //    StatusChanged?.Invoke(this, EventArgs.Empty);
        //}
    }
}