using NUnit.Framework;
using TimeOffManager.Models;
using System;

namespace TimeOffManager.Test
{
    public class EmployeeTests
    {
        [Test]
        public void Constructor_SetsNameAndSurname_FromBaseClass()
        {
            var employee = new Employee("Robert", "Kowalski");

            Assert.That(employee.Name, Is.EqualTo("Robert"));
            Assert.That(employee.Surname, Is.EqualTo("Kowalski"));
        }

        [Test]
        public void AvailableDays_DefaultsTo30()
        {
            var employee = new Employee("Test", "User");

            Assert.That(employee.AvailableDays, Is.EqualTo(30));
        }

        [Test]
        public void LeaveHistory_IsInitiallyEmpty()
        {
            var employee = new Employee("Test", "User");

            Assert.That(employee.LeaveHistory, Is.Empty);
        }

        [Test]
        public void IsOnLeave_ReturnsTrue_IfApprovedLeaveCoversToday()
        {
            var employee = new Employee("Test", "User");
            var today = new DateTime(2025, 8, 15);

            employee.LeaveHistory.Add(new LeaveRequest
            {
                StartDate = new DateTime(2025, 8, 14),
                EndDate = new DateTime(2025, 8, 16),
                Status = LeaveStatus.Approved
            });

            Assert.That(employee.IsOnLeave(today), Is.True);
        }

        [Test]
        public void IsOnLeave_ReturnsFalse_IfNoApprovedLeaveCoversToday()
        {
            var employee = new Employee("Test", "User");
            var today = new DateTime(2025, 8, 15);

            employee.LeaveHistory.Add(new LeaveRequest
            {
                StartDate = new DateTime(2025, 8, 10),
                EndDate = new DateTime(2025, 8, 12),
                Status = LeaveStatus.Approved
            });

            Assert.That(employee.IsOnLeave(today), Is.False);
        }
    }
}
