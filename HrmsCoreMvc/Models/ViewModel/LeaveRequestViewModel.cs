using HrmsCoreMvc.Models.Leave;

namespace HrmsCoreMvc.Models.ViewModel
{
    public class LeaveRequestViewModel
    {
        public LeaveRequest LeaveRequest { get; set; } = new LeaveRequest();

        public List<LeaveRequest> LeaveRequests { get; set; } = new();
    }
}
