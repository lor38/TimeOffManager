using NUnit.Framework;
using TimeOffManager.Models;
using System;

namespace TimeOffManager.Test
{
    public class LeaveRequestTests
    {
        [Test]
        public void Duration_ReturnsCorrectNumberOfDays()
        {
            // Arrange
            var employee = new Employee("Robert", "Lorenc");
            var request = new LeaveRequest
            {
                RequestedBy = employee,
                StartDate = new DateTime(2025, 8, 14),
                EndDate = new DateTime(2025, 8, 16)
            };

            // Act
            int duration = request.Duration;

            // Assert
            Assert.That(duration, Is.EqualTo(3));
        }

        [Test]
        public void ToString_ReturnsFormattedString()
        {
            var request = new LeaveRequest
            {
                StartDate = new DateTime(2025, 8, 14),
                EndDate = new DateTime(2025, 8, 16),
                Type = LeaveType.Wypoczynkowy,
                Status = LeaveStatus.Approved,
                Comment = "Wakacje"
            };

            string expected = "2025-08-14 → 2025-08-16 | Wypoczynkowy | Approved | Wakacje";

            Assert.That(request.ToString(), Is.EqualTo(expected));
        }
    }
}
