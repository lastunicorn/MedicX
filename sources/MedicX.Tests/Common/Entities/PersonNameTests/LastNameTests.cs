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
    public class LastNameTests
    {
        [Test]
        public void set_get_LastName()
        {
            PersonName personName = new PersonName();
            personName.LastName = "Dumitrescu";

            Assert.That(personName.LastName, Is.EqualTo("Dumitrescu"));
        }

        [Test]
        public void raises_Changed_event()
        {
            PersonName personName = new PersonName();
            bool isChanged = false;
            personName.Changed += (sender, e) => isChanged = true;

            personName.LastName = "Dumitrescu";

            Assert.That(isChanged, Is.True);
        }
    }
}