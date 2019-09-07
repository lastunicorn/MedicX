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

namespace DustInTheWind.MedicX.Tests.Common.Entities.MedicalEventTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_MedicalEvent_objects_are_not_reference_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent();
            MedicalEvent medicalEvent2 = new MedicalEvent();

            bool actual = medicalEvent1 == medicalEvent2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_default_values_are_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent();
            MedicalEvent medicalEvent2 = new MedicalEvent();

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_same_values_are_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
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
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_ID_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
            {
                Id = new Guid("4ad661ed-9970-4b28-96e7-b46c5426e50a"),
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
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_Date_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(3000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_Medic_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 2"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 1"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_Clinic_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
            {
                Id = new Guid("1c088cbe-5d23-4ea6-b645-d09098936743"),
                Date = new DateTime(2000, 10, 10),
                Medic = new Medic
                {
                    Name = "Medic 1"
                },
                Clinic = new Clinic
                {
                    Name = "Clinic 2"
                },
                Labels = new ObservableCollection<string> { "label1", "label2" },
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_Labels_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
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
                Labels = new ObservableCollection<string> { "label3", "label4" },
                Comments = "some comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_MedicalEvent_objects_with_different_Comment_are_not_equal()
        {
            MedicalEvent medicalEvent1 = new MedicalEvent
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
                Comments = "some comments"
            };
            MedicalEvent medicalEvent2 = new MedicalEvent()
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
                Comments = "some other comments"
            };

            bool actual = Equals(medicalEvent1, medicalEvent2);

            Assert.That(actual, Is.False);
        }
    }
}