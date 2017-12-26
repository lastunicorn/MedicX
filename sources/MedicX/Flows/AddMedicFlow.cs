﻿// MedicX
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
using System.Collections.Generic;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Flows
{
    internal class AddMedicFlow : IFlow
    {
        private readonly UnitOfWork unitOfWork;

        public AddMedicFlow(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            this.unitOfWork = unitOfWork;
        }

        public void Run()
        {
            Medic medic = new Medic
            {
                Id = 0,
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    LastName = "Castravete"
                },
                Specializations = new List<string>
                {
                    "murături",
                    "varză"
                }
            };

            unitOfWork.MedicRepository.Add(medic);
        }
    }
}