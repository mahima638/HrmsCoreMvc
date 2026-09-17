using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace HrmsCoreMvc.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext db;

        public LeaveService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddLeaveType(MasterLeaveType m)
        {
            m.Status = "Active";
            db.MasterLeaveTypes.Add(m);
            await db.SaveChangesAsync();   

        }

        public async Task AllocateLeaveDeptwise(int deptId, int leaveTypeId, int noOfLeaves)
        {
            var deptLeave=await db.DepartmentLeaves
                .FirstOrDefaultAsync(x=>
                x.DepartmentId== deptId && x.LeaveTypeId == leaveTypeId);

            if (deptLeave == null)
            {
                var newDeptLeave = new DepartmentLeaves
                {
                    DepartmentId = deptId,
                    LeaveTypeId = leaveTypeId,
                    LeavesCount = noOfLeaves,
                    Status = "Active"
                };
                db.DepartmentLeaves.Add(newDeptLeave);

            }
            else 
            {
                deptLeave.LeavesCount = noOfLeaves;
                deptLeave.Status = "Active";
                db.DepartmentLeaves.Update(deptLeave);
            }
            await db.SaveChangesAsync();


        }

        public async Task DeleteLeaveType(int leaveTypeId)
        {
            var del = db.MasterLeaveTypes.Find(leaveTypeId);
            if (del != null)
            {
                db.MasterLeaveTypes.Remove(del);
               await db.SaveChangesAsync();
            }  
        }

        public async Task<List<SelectListItem>> FetchDept()
        {
            return await db.department
                 .Where(x => x.Status == "Active")
                 .Select(x => new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.DepartmentId.ToString()
                 }).ToListAsync();
        }

        public async Task<List<SelectListItem>> FetchLeaveType()
        {
            return await db.MasterLeaveTypes
                .Where(x => x.Status == "Active")
                 .Select(x => new SelectListItem
                 {
                     Text = x.LeaveType,
                     Value = x.LeaveTypeId.ToString()
                 }).ToListAsync();
        }

        public async Task<List<MasterLeaveType>> FetchLeaveTypeList()
        {
            return await db.MasterLeaveTypes.ToListAsync();
        }


    }
}
