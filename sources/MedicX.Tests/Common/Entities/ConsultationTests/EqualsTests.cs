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

namespace DustInThyeWind.MedicX.Tests.Common.Entities.ConsultationTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Consultation_objects_are_not_reference_equal()
        {
            Consultation consultation1 = new Consultation();
            Consultation consultation2 = new Consultation();

            bool actual = consultation1 == consultation2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Consultation_objects_with_default_values_are_equal()
        {
            Consultation consultation1 = new Consultation();
            Consultation consultation2 = new Consultation();

            bool actual = Equals(consultation1, consultation2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Consultation_objects_with_same_values_are_equal()
        {
            Consultation consultation1 = new Consultation
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Prescriptions = new ObservableCollection<Prescription>
                {
                    new Prescription { Description = "prescription1", Result = new object() },
                    new Prescription { Description = "prescription2", Result = new object() }
                },
                Comments = "some comments"
            };
            Consultation consultation2 = new Consultation()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Prescriptions = new ObservableCollection<Prescription>
                {
                    new Prescription { Description = "prescription1", Result = new object() },
                    new Prescription { Description = "prescription2", Result = new object() }
                },
                Comments = "some comments"
            };

            bool actual = Equals(consultation1, consultation2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Consultation_objects_with_different_Prescriptions_are_not_equal()
        {
            Consultation consultation1 = new Consultation
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Prescriptions = new ObservableCollection<Prescription>
                {
                    new Prescription { Description = "prescription1", Result = new object() },
                    new Prescription { Description = "prescription2", Result = new object() }
                },
                Comments = "some comments"
            };
            Consultation consultation2 = new Consultation()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Prescriptions = new ObservableCollection<Prescription>
                {
                    new Prescription { Description = "prescription3", Result = new object() },
                    new Prescription { Description = "prescription4", Result = new object() }
                },
                Comments = "some comments"
            };

            bool actual = Equals(consultation1, consultation2);

            Assert.That(actual, Is.False);
        }
    }
}