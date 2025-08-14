using NUnit.Framework;
using TimeOffManager.Models;
using System;

namespace TimeOffManager.Test
{
    public class LeaveTypeTests
    {
        [Test]
        public void Enum_DefinesAllExpectedValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Enum.IsDefined(typeof(LeaveType), "Wypoczynkowy"));
                Assert.That(Enum.IsDefined(typeof(LeaveType), "NaZadanie"));
                Assert.That(Enum.IsDefined(typeof(LeaveType), "Bezplatny"));
                Assert.That(Enum.IsDefined(typeof(LeaveType), "Chorobowy"));
                Assert.That(Enum.IsDefined(typeof(LeaveType), "Okolicznościowy"));
            });
        }

        [Test]
        public void ToString_ReturnsCorrectName()
        {
            var type = LeaveType.Chorobowy;
            Assert.That(type.ToString(), Is.EqualTo("Chorobowy"));
        }

        [Test]
        public void Parse_ValidString_ReturnsCorrectEnum()
        {
            var parsed = Enum.Parse<LeaveType>("Bezplatny");
            Assert.That(parsed, Is.EqualTo(LeaveType.Bezplatny));
        }

        [Test]
        public void Parse_InvalidString_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Enum.Parse<LeaveType>("Na żądanie"); 
            });
        }
    }
}
