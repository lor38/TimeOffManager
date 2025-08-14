using TimeOffManager.Interfaces;

namespace TimeOffManager.Models
{
    public class Supervisor : Employee, ILeaveApprover
    {
        public Supervisor(string name, string surname) : base(name, surname) { }

        public void ApproveRequest(LeaveRequest request)
        {
            request.Status = LeaveStatus.Approved;
            request.RequestedBy.AvailableDays -= request.Duration;
        }

        public void RejectRequest(LeaveRequest request)
        {
            request.Status = LeaveStatus.Rejected;
        }
    }
}
