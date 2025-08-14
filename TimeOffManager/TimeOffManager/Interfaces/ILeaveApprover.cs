using TimeOffManager.Models;

namespace TimeOffManager.Interfaces
{
    public interface ILeaveApprover
    {
        void ApproveRequest(LeaveRequest request);
        void RejectRequest(LeaveRequest request);
    }
}
