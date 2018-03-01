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

using DustInTheWind.MedicX.Common.Entities;
using NUnit.Framework;

namespace DustInThyeWind.MedicX.Tests.Common.Entities.PersonNameTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Medic_objects_are_not_reference_equal()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName();

            bool actual = personName1 == personName2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Medic_objects_with_default_values_are_equal()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName();

            bool actual = Equals(personName1, personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Medic_objects_with_same_values_are_equal()
        {
            PersonName personName1 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Olar"
            };
            PersonName personName2 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Olar"
            };

            bool actual = Equals(personName1, personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Medic_objects_with_different_FirstName_values_are_not_equal()
        {
            PersonName personName1 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Olar"
            };
            PersonName personName2 = new PersonName
            {
                FirstName = "Marius",
                MiddleName = "Robert",
                LastName = "Olar"
            };

            bool actual = Equals(personName1, personName2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Medic_objects_with_different_MiddleName_values_are_not_equal()
        {
            PersonName personName1 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Olar"
            };
            PersonName personName2 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Paul",
                LastName = "Olar"
            };

            bool actual = Equals(personName1, personName2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Medic_objects_with_different_LastName_values_are_not_equal()
        {
            PersonName personName1 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Olar"
            };
            PersonName personName2 = new PersonName
            {
                FirstName = "Vasile",
                MiddleName = "Robert",
                LastName = "Konya"
            };

            bool actual = Equals(personName1, personName2);

            Assert.That(actual, Is.False);
        }
    }
}