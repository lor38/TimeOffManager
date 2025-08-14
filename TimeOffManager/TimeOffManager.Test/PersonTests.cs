using NUnit.Framework;
using TimeOffManager.Models;

namespace TimeOffManager.Test
{
    public class PersonTests
    {
        [Test]
        public void Constructor_SetsNameAndSurnameCorrectly()
        {
            var person = new Person("Robert", "Kowalski");

            Assert.That(person.Name, Is.EqualTo("Robert"));
            Assert.That(person.Surname, Is.EqualTo("Kowalski"));
        }

        [Test]
        public void ToString_ReturnsFullName()
        {
            var person = new Person("Anna", "Nowak");

            string result = person.ToString();

            Assert.That(result, Is.EqualTo("Anna Nowak"));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_WhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Person(null!, "Nowak"));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_WhenSurnameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Person("Anna", null!));
        }
    }
}
