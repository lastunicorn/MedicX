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

using DustInTheWind.MedicX.Domain.Entities;
using NUnit.Framework;

namespace DustInTheWind.MedicX.Tests.Common.Entities.PersonNameTests
{
    [TestFixture]
    public class ContainsTests
    {
        [Test]
        public void search_FirstName()
        {
            PersonName personName = new PersonName
            {
                FirstName = "Flavius",
                MiddleName = "Bogdan",
                LastName = "Dumitrescu"
            };

            bool actual = personName.Contains("avi");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void search_MiddleName()
        {
            PersonName personName = new PersonName
            {
                FirstName = "Flavius",
                MiddleName = "Bogdan",
                LastName = "Dumitrescu"
            };

            bool actual = personName.Contains("ogd");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void search_LastName()
        {
            PersonName personName = new PersonName
            {
                FirstName = "Flavius",
                MiddleName = "Bogdan",
                LastName = "Dumitrescu"
            };

            bool actual = personName.Contains("itre");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void search_inexistent_text()
        {
            PersonName personName = new PersonName
            {
                FirstName = "Flavius",
                MiddleName = "Bogdan",
                LastName = "Dumitrescu"
            };

            bool actual = personName.Contains("aaa");

            Assert.That(actual, Is.False);
        }
    }
}