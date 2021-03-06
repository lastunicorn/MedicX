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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Persistence.Translators
{
    internal static class MedicExtensions
    {
        public static List<Medic> Translate(this IEnumerable<Json.Entities.Medic> medics)
        {
            return medics?
                .Select(Translate)
                .ToList();
        }

        public static Medic Translate(this Json.Entities.Medic medic)
        {
            if (medic == null)
                return null;

            Medic translatedMedic = new Medic
            {
                Id = medic.Id,
                Name = medic.Name.Translate(),
                Specializations = new ObservableCollection<string>(medic.Specializations),
                Comments = medic.Comments
            };

            translatedMedic.AcceptChanges();

            return translatedMedic;
        }

        public static List<Json.Entities.Medic> Translate(this IEnumerable<Medic> medics)
        {
            return medics?
                .Select(Translate)
                .ToList();
        }

        public static Json.Entities.Medic Translate(this Medic medic)
        {
            if (medic == null)
                return null;

            return new Json.Entities.Medic
            {
                Id = medic.Id,
                Name = medic.Name.Translate(),
                Comments = medic.Comments,
                Specializations = medic.Specializations?.ToList()
            };
        }
    }
}