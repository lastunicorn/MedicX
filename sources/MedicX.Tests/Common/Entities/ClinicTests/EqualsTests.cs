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
using DustInTheWind.MedicX.Domain.Entities;
using NUnit.Framework;

namespace DustInTheWind.MedicX.Tests.Common.Entities.ClinicTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Clinic_objects_are_not_reference_equal()
        {
            Clinic clinic1 = new Clinic();
            Clinic clinic2 = new Clinic();

            bool actual = clinic1 == clinic2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_default_values_are_equal()
        {
            Clinic clinic1 = new Clinic();
            Clinic clinic2 = new Clinic();

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Clinic_objects_with_same_values_are_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic 1",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic 1",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_ID_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic 1",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic()
            {
                Id = new Guid("4ad661ed-9970-4b28-96e7-b46c5426e50a"),
                Name = "Life Clinic 1",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_Name_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic 1",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic 2",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_Address_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 2",
                    City = "City 2",
                    County = "County 2",
                    Country = "Country 2"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_Phones_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone3", "phone4" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_Program_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Tuesday - 8:00-20:00",
                Comments = "some comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Clinic_objects_with_different_Comment_are_not_equal()
        {
            Clinic clinic1 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some comments"
            };
            Clinic clinic2 = new Clinic
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Name = "Life Clinic",
                Address = new Address
                {
                    Street = "Street 1",
                    City = "City 1",
                    County = "County 1",
                    Country = "Country 1"
                },
                Phones = new ObservableCollection<string> { "phone1", "phone2" },
                Program = "Monday - 8:00-20:00",
                Comments = "some other comments"
            };

            bool actual = Equals(clinic1, clinic2);

            Assert.That(actual, Is.False);
        }
    }
}