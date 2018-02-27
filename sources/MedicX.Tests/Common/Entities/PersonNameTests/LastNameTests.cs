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