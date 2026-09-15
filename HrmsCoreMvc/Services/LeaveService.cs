using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace HrmsCoreMvc.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext db;

        public LeaveService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddLeaveType(MasterLeaveType m)
        {
            m.Status = "Active";
            db.MasterLeaveTypes.Add(m);
            db.SaveChanges();   

        }

        public void AllocateLeaveDeptwise(int deptId, int leaveTypeId, int noOfLeaves)
        {
            
        }

        public List<SelectListItem> FetchDept()
        {
            return db.department
                 .Where(x => x.Status == "Active")
                 .Select(x => new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.DepartmentId.ToString()
                 }).ToList();
        }

        public List<SelectListItem> FetchLeaveType()
        {
            return db.MasterLeaveTypes
                 .Select(x => new SelectListItem
                 {
                     Text = x.LeaveType,
                     Value = x.LeaveTypeId.ToString()
                 }).ToList();
        }

        public List<MasterLeaveType> FetchLeaveTypeList()
        {
            return db.MasterLeaveTypes.ToList();
        }
    }
}
