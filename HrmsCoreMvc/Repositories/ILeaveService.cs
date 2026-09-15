using HrmsCoreMvc.Models.Leave;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Repositories
{
    public interface ILeaveService
    {
        public void AddLeaveType(MasterLeaveType m);
        List<SelectListItem> FetchDept();

        List<SelectListItem> FetchLeaveType();

        List<MasterLeaveType> FetchLeaveTypeList();

        public void AllocateLeaveDeptwise(int deptId,int leaveTypeId,int noOfLeaves);
    }
}
