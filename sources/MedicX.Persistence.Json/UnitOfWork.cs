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
using DustInTheWind.MedicX.Persistence.Json.Translators;

namespace DustInTheWind.MedicX.Persistence.Json
{
    public class UnitOfWork : IDisposable
    {
        private static int instanceCount;
        private static readonly object SyncObject = new object();

        private bool isDisposed;

        private readonly JsonDatabase jsonDatabase;
        private IMedicRepository medicRepository;
        private IConsultationsRepository consultationsRepository;
        private IClinicRepository clinicRepository;
        private readonly MedicXData medicXData;

        public IMedicRepository MedicRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (medicRepository == null)
                    medicRepository = new MedicRepository(medicXData);

                return medicRepository;
            }
        }

        public IConsultationsRepository ConsultationsRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (consultationsRepository == null)
                    consultationsRepository = new ConsultationsRepository(medicXData);

                return consultationsRepository;
            }
        }

        public IClinicRepository ClinicRepository
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (clinicRepository == null)
                    clinicRepository = new ClinicRepository(medicXData);

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

                jsonDatabase = new JsonDatabase();

                Entities.MedicXData data = jsonDatabase.Open();
                medicXData = data.Translate();

                instanceCount++;
            }
        }

        public void Save()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            Entities.MedicXData data = medicXData.Translate();
            jsonDatabase.Save(data);
        }

        public void Dispose()
        {
            lock (SyncObject)
                instanceCount--;

            isDisposed = true;
        }
    }
}
