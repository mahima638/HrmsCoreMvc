using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Models.Projects;      
using HrmsCoreMvc.Models.PayRoll;     
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace HrmsCoreMvc.Services
{
    public class TimesheetService:ITimesheetService
    {
        private readonly ApplicationDbContext db;
        public TimesheetService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<AllProjects>> GetProjects()
        {
            return await db.AllProjects.ToListAsync();

        }

        public async Task<List<Timesheet>> GetTimesheets()
        {
            return await db.Timesheets
         .Include(t => t.User)
         .Include(t => t.Project)
         .ToListAsync();
        }

        public async Task AddTimesheet(Timesheet t)
        {
            await db.Timesheets.AddAsync(t);
            await db.SaveChangesAsync();
        }

        public async Task SendForApproval(List<int> ids)
        {
            var timesheets = await db.Timesheets
                .Where(t => ids.Contains(t.TimesheetId))
                .ToListAsync();

            foreach (var item in timesheets)
            {
                item.Status = "Pending Approval";
            }

            await db.SaveChangesAsync();
        }

        public async Task ApproveSelected(List<int> ids)
        {
            var timesheets = await db.Timesheets
                .Where(t => ids.Contains(t.TimesheetId))
                .ToListAsync();

            foreach (var item in timesheets)
            {
                item.Status = "Approved";
                item.ApprovedAt = DateTime.Now;
                item.ApprovedBy = "admin@example.com";
                //item.ApprovedBy = HttpContext.Session.GetString("Email"); 
            }

            await db.SaveChangesAsync();
        }

        public async Task RejectSelected(List<int> ids)
        {
            var timesheets = await db.Timesheets
                .Where(t => ids.Contains(t.TimesheetId))
                .ToListAsync();

            foreach (var item in timesheets)
            {
                item.Status = "Rejected";
                item.ApprovedAt = DateTime.Now;
                item.ApprovedBy = "admin@example.com"; 
                //item.ApprovedBy = HttpContext.Session.GetString("Email"); 
            }

            await db.SaveChangesAsync();
        }
    }
}
