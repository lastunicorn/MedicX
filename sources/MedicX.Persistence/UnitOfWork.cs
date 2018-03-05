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

namespace DustInTheWind.MedicX.Persistence
{
    public class UnitOfWork : IDisposable
    {
        private static int instanceCount;
        private static readonly object SyncObject = new object();

        private bool isDisposed;

        private readonly Storage storage;
        private IMedicRepository medicRepository;
        private IConsultationRepository consultationRepository;
        private IInvestigationRepository investigationRepository;
        private IClinicRepository clinicRepository;

        public IMedicRepository MedicRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (medicRepository == null)
                    medicRepository = new MedicRepository(storage);

                return medicRepository;
            }
        }

        public IConsultationRepository ConsultationRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (consultationRepository == null)
                    consultationRepository = new ConsultationRepository(storage);

                return consultationRepository;
            }
        }

        public IInvestigationRepository InvestigationRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (investigationRepository == null)
                    investigationRepository = new InvestigationRepository(storage);

                return investigationRepository;
            }
        }

        public IClinicRepository ClinicRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (clinicRepository == null)
                    clinicRepository = new ClinicRepository(storage);

                return clinicRepository;
            }
        }

        public UnitOfWork()
        {
            if (instanceCount > 0)
                throw new Exception("Another instance of the UnitOfWork already exists.");

            lock (SyncObject)
            {
                if (instanceCount > 0)
                    throw new Exception("Another instance of the UnitOfWork already exists.");

                storage = new Storage();
                storage.Open();

                instanceCount++;
            }
        }

        public void Save()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            storage.Save();
        }

        public void Dispose()
        {
            lock (SyncObject)
            {
                instanceCount--;
            }

            isDisposed = true;
        }
    }
}