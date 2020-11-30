﻿// MedicX
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

using System.Collections.Generic;
using DustInTheWind.MedicX.Domain.Entities;

namespace DustInTheWind.MedicX.Application.AddNewClinic
{
    public class AddNewClinicRequest
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<string> Phones { get; set; }

        public string Program { get; set; }

        public string Comments { get; set; }
    }
}