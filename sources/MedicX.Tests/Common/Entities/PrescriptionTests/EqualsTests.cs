// PrescriptionX
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

namespace DustInThyeWind.MedicX.Tests.Common.Entities.PrescriptionTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void two_different_Prescription_objects_are_not_reference_equal()
        {
            Prescription prescription1 = new Prescription();
            Prescription prescription2 = new Prescription();

            bool actual = prescription1 == prescription2;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_different_Prescription_objects_with_default_values_are_equal()
        {
            Prescription prescription1 = new Prescription();
            Prescription prescription2 = new Prescription();

            bool actual = Equals(prescription1, prescription2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Prescription_objects_with_same_values_are_equal()
        {
            Prescription prescription1 = new Prescription
            {
                Description = "prescription 1"
            };
            Prescription prescription2 = new Prescription
            {
                Description = "prescription 1"
            };

            bool actual = Equals(prescription1, prescription2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_different_Prescription_objects_with_different_Description_are_not_equal()
        {
            Prescription prescription1 = new Prescription
            {
                Description = "prescription 1"
            };
            Prescription prescription2 = new Prescription
            {
                Description = "prescription 2"
            };

            bool actual = Equals(prescription1, prescription2);

            Assert.That(actual, Is.False);
        }
    }
}