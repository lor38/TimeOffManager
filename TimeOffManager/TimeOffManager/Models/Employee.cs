using System;
using System.Collections.Generic;

namespace TimeOffManager.Models
{
    public class Employee : Person
    {
        public int AvailableDays { get; set; } = 30;
        public List<LeaveRequest> LeaveHistory { get; } = new();

        public Supervisor? Supervisor { get; set; }

        public Employee(string name, string surname) : base(name, surname) { }

        public bool IsOnLeave(DateTime today)
        {
            return LeaveHistory.Exists(lr =>
                lr.Status == LeaveStatus.Approved &&
                lr.StartDate <= today &&
                lr.EndDate >= today);
        }
    }
}
