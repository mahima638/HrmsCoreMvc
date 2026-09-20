using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<DepartmentLeaves>> FetchDeptLeaveDetails()
        {
            return await db.DepartmentLeaves
                .Include(x => x.Department)
                .Include(x => x.MasterLeaveType)
                .ToListAsync();   
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

  

        public async Task UpdateLeaveTypeStatus(int leaveTypeId, bool isActive)
        {
            var id= await db.MasterLeaveTypes.FindAsync(leaveTypeId);
            if (id!= null)
            { 
                id.Status = isActive ? "Active" : "Inactive";
                await db.SaveChangesAsync();
            }
        }

        public async Task ApplyLeave(LeaveRequest req)
        {
            //req.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            req.UserId = 2;

            req.NumberOfDays = (req.EndDate.Date - req.StartDate.Date).Days + 1;
            req.Status = "Pending";

            db.LeaveRequests.Add(req);
            await db.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> FetchLeaveRequests(int id)
        {
            return await db.LeaveRequests
                .Include(x => x.MasterLeaveType)
                .Where(x => x.UserId == id)
                .ToListAsync();
        }



        public async Task<List<LeaveRequest>> FetchManagerLeaveRequests()
        {
            return await db.LeaveRequests
                .Include(x => x.User)
                .Include(x => x.MasterLeaveType)
                .OrderByDescending(x => x.LeaveRequestId)
                .ToListAsync();
        }

        public async Task ApproveLeave(int leaveRequestId, string managerName)
        {
            var leave = await db.LeaveRequests.FindAsync(leaveRequestId);

            if (leave == null)
                return;

            leave.Status = "Approved";
            leave.ApprovedBy = managerName;
            leave.StatusHistory = $"Approved on {DateTime.Now:g}";

            await db.SaveChangesAsync();
        }

        public async Task RejectLeave(int leaveRequestId, string managerName)
        {
            var leave = await db.LeaveRequests.FindAsync(leaveRequestId);

            if (leave == null)
                return;

            leave.Status = "Rejected";
            leave.ApprovedBy = managerName;
            leave.StatusHistory = $"Rejected on {DateTime.Now:g}";

            await db.SaveChangesAsync();
        }

        public async Task<MasterLeaveType> GetLeaveTypeById(int id)
        {
            return await db.MasterLeaveTypes.FirstOrDefaultAsync(x => x.LeaveTypeId == id);
        }

        public async Task UpdateLeaveType(MasterLeaveType model)
        {
            var leave = await db.MasterLeaveTypes
                .FirstOrDefaultAsync(x => x.LeaveTypeId == model.LeaveTypeId);

            if (leave != null)
            {
                leave.LeaveType = model.LeaveType;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteDepartmentLeave(int id)
        {
            var deptLeave = await db.DepartmentLeaves.FindAsync(id);
            if (deptLeave != null)
            {
                db.DepartmentLeaves.Remove(deptLeave);
                await db.SaveChangesAsync();
            }
        }

    }
}
