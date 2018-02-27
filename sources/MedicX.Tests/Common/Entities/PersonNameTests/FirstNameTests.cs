using DustInTheWind.MedicX.Common.Entities;
using NUnit.Framework;

namespace DustInThyeWind.MedicX.Tests.Common.Entities.PersonNameTests
{
    [TestFixture]
    public class FirstNameTests
    {
        [Test]
        public void set_get_FirstName()
        {
            PersonName personName = new PersonName();
            personName.FirstName = "Flavius";

            Assert.That(personName.FirstName, Is.EqualTo("Flavius"));
        }

        [Test]
        public void raises_Changed_event()
        {
            PersonName personName = new PersonName();
            bool isChanged = false;
            personName.Changed += (sender, e) => isChanged = true;

            personName.FirstName = "Flavius";

            Assert.That(isChanged, Is.True);
        }
    }
}