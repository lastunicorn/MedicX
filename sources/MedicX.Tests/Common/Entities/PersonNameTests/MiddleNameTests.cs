using DustInTheWind.MedicX.Common.Entities;
using NUnit.Framework;

namespace DustInThyeWind.MedicX.Tests.Common.Entities.PersonNameTests
{
    [TestFixture]
    public class MiddleNameTests
    {
        [Test]
        public void set_get_MiddleName()
        {
            PersonName personName = new PersonName();
            personName.MiddleName = "Bogdan";

            Assert.That(personName.MiddleName, Is.EqualTo("Bogdan"));
        }

        [Test]
        public void raises_Changed_event()
        {
            PersonName personName = new PersonName();
            bool isChanged = false;
            personName.Changed += (sender, e) => isChanged = true;

            personName.MiddleName = "Bogdan";

            Assert.That(isChanged, Is.True);
        }
    }
}