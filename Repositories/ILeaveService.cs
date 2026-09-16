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
    }
}
