using HrmsCoreMvc.Models.Leave;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Repositories
{
    public interface ILeaveService
    {
        public Task AddLeaveType(MasterLeaveType m);
        Task<List<SelectListItem>> FetchDept();

        Task<List<SelectListItem>> FetchLeaveType();

        Task<List<MasterLeaveType>> FetchLeaveTypeList();

        public Task AllocateLeaveDeptwise(int deptId,int leaveTypeId,int noOfLeaves);

        public Task DeleteLeaveType(int leaveTypeId);

        Task<List<DepartmentLeaves>> FetchDeptLeaveDetails();

        public Task UpdateLeaveTypeStatus(int leaveTypeId, bool isActive);

        public Task ApplyLeave(LeaveRequest req);

        Task<List<LeaveRequest>> FetchLeaveRequests(int id);
        Task<List<LeaveRequest>> FetchManagerLeaveRequests();

        public Task ApproveLeave(int leaveRequestId, string managerName);
        public Task RejectLeave(int leaveRequestId, string managerName);

        Task<MasterLeaveType> GetLeaveTypeById(int id);

        public Task UpdateLeaveType(MasterLeaveType model);
        Task DeleteDepartmentLeave(int id);



    }
}
