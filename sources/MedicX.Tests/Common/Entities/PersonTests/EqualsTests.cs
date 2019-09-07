// PersonX
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
using DustInTheWind.MedicX.Domain.Entities;
using NUnit.Framework;

namespace DustInTheWind.MedicX.Tests.Common.Entities.PersonTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Person_objects_are_not_reference_equal()
        {
            Person person1 = new Person();
            Person person2 = new Person();

            bool actual = person1 == person2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Person_objects_with_default_values_are_equal()
        {
            Person person1 = new Person();
            Person person2 = new Person();

            bool actual = Equals(person1, person2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Person_objects_with_same_values_are_equal()
        {
            Person person1 = new Person
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };
            Person person2 = new Person()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };

            bool actual = Equals(person1, person2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Person_objects_with_different_ID_are_not_equal()
        {
            Person person1 = new Person
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };
            Person person2 = new Person()
            {
                Id = new Guid("4ad661ed-9970-4b28-96e7-b46c5426e50a"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };

            bool actual = Equals(person1, person2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Person_objects_with_different_Name_are_not_equal()
        {
            Person person1 = new Person
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };
            Person person2 = new Person()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "George",
                    MiddleName = "Marian",
                    LastName = "Anghel"
                },
                Comments = "some comments"
            };

            bool actual = Equals(person1, person2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Person_objects_with_different_Comments_are_not_equal()
        {
            Person person1 = new Person
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some comments"
            };
            Person person2 = new Person()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = new PersonName
                {
                    FirstName = "Vasile",
                    MiddleName = "Robert",
                    LastName = "Olar"
                },
                Comments = "some other comments"
            };

            bool actual = Equals(person1, person2);

            Assert.That(actual, Is.False);
        }
    }
}