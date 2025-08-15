using System;

namespace TimeOffManager.Models
{
    public class LeaveRequest
    {
        public LeaveType Type { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public Employee RequestedBy { get; set; }

        public int Duration => (EndDate - StartDate).Days + 1;

        public override string ToString()
        {
            return $"{StartDate:dd-MM-yyyy} → {EndDate:dd-MM-yyyy} | {Type} | {Status} | {Comment}";
        }
    }
}
