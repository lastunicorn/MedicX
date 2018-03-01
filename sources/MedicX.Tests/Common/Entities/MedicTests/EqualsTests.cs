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
using System.Collections.ObjectModel;
using DustInTheWind.MedicX.Common.Entities;
using NUnit.Framework;

namespace DustInThyeWind.MedicX.Tests.Common.Entities.MedicTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Medic_objects_are_not_reference_equal()
        {
            Medic medic1 = new Medic();
            Medic medic2 = new Medic();

            bool actual = medic1 == medic2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Medic_objects_with_default_values_are_equal()
        {
            Medic medic1 = new Medic();
            Medic medic2 = new Medic();

            bool actual = Equals(medic1, medic2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Medic_objects_with_same_values_are_equal()
        {
            Medic medic1 = new Medic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Specializations = new ObservableCollection<string> { "specialization1", "specialization2" },
                Comments = "some comments"
            };
            Medic medic2 = new Medic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Specializations = new ObservableCollection<string> { "specialization1", "specialization2" },
                Comments = "some comments"
            };

            bool actual = Equals(medic1, medic2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Medic_objects_with_different_Specializations_are_not_equal()
        {
            Medic medic1 = new Medic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Specializations = new ObservableCollection<string> { "specialization1", "specialization2" },
                Comments = "some comments"
            };
            Medic medic2 = new Medic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Specializations = new ObservableCollection<string> { "specialization3", "specialization4" },
                Comments = "some comments"
            };

            bool actual = Equals(medic1, medic2);

            Assert.That(actual, Is.False);
        }
    }
}