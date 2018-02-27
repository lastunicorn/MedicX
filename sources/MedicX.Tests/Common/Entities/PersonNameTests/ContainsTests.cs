using DustInTheWind.MedicX.Common.Entities;
using NUnit.Framework;

namespace DustInThyeWind.MedicX.Tests.Common.Entities.PersonNameTests
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