// MedicX
// Copyright (C) 2017 Dust in the Wind
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

namespace DustInTheWind.MedicX.Persistence.Json
{
    public class UnitOfWork : IDisposable
    {
        private static int instanceCount;
        private static readonly object syncObject = new object();

        private readonly JsonDatabase jsonDatabase;
        private IMedicRepository medicRepository;

        public IMedicRepository MedicRepository
        {
            get
            {
                if (medicRepository == null)
                    medicRepository = new MedicRepository(jsonDatabase);

                return medicRepository;
            }
        }

        public UnitOfWork()
        {
            if (instanceCount > 0)
                throw new Exception("Another instance of the UnitOfWork already exists.");

            lock (syncObject)
            {
                if (instanceCount > 0)
                    throw new Exception("Another instance of the UnitOfWork already exists.");

                jsonDatabase = new JsonDatabase();
                jsonDatabase.Open();

                instanceCount++;
            }
        }

        public void Save()
        {
            jsonDatabase.Save();
        }

        public void Dispose()
        {
            lock (syncObject)
                instanceCount--;
        }
    }
}
