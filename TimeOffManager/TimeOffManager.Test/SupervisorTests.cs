using NUnit.Framework;
using TimeOffManager.Models;

namespace TimeOffManager.Test
{
    public class SupervisorTests
    {
        [Test]
        public void Constructor_SetsNameAndSurname()
        {
            var supervisor = new Supervisor("Jan", "Nowak");

            Assert.That(supervisor.Name, Is.EqualTo("Jan"));
            Assert.That(supervisor.Surname, Is.EqualTo("Nowak"));
        }

        [Test]
        public void ToString_ReturnsFullName()
        {
            var supervisor = new Supervisor("Jan", "Nowak");

            Assert.That(supervisor.ToString(), Is.EqualTo("Jan Nowak"));
        }
    }
}
