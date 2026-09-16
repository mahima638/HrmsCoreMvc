using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HrmsCoreMvc.Services.Reports
{
    public class DailyReportService : IDailyReportService
    {
        private readonly ApplicationDbContext db;

        public DailyReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> fetchTotalAbsent()
        {
            var data = await db.Attendance.Select(a => a.Status == "Absent").CountAsync();
            return data;

        }

        public async Task<int> fetchTotalPresent()
        {
            var data = await db.Attendance.Select(a => a.Status == "Present").CountAsync();
            return data;
        }

        public async Task<int> fetchCompletedTasks()
        {
            var data = await db.tasks.Where(t => t.Status == "Completed").CountAsync();
            return data;
        }

        public async Task<int> fetchPendingTasks()
        {
            var data = await db.tasks.Where(t => t.Status == "Pending").CountAsync();
            return data;
        }

        public async Task<IEnumerable<DailyAttendanceViewModel>> fetchDailyTasks()
        {
            return await db.Attendance.Include(a => a.User).Select(a => new DailyAttendanceViewModel
            {
                name = a.User.FirstName + " " + a.User.LastName,
                Date = a.Date,
                Department = a.User != null && a.User.departments != null ? a.User.departments.Name : string.Empty,
                Status = a.Status
            }).ToListAsync();
        }

        public async Task<IEnumerable<DailyAttendanceViewModel>> sortDailyTasks(string? status, string? sortType)
        {
            var query = db.Attendance.Include(a => a.User).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.Status == status);
            }

            if (sortType == "Ascending")
            {
                query = query.OrderBy(a => a.Date);
            }
            else if (sortType == "Descending")
            {
                query = query.OrderByDescending(a => a.Date);

            }
            else if (sortType == "Last 7 days")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddDays(-7));
            }
            else if (sortType == "Last Month")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddMonths(-1));
            }
            else if (sortType == "Recently Added")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddDays(-1));

            }

            var result = await query.Select(a => new DailyAttendanceViewModel
            {
                name = a.User.FirstName + " " + a.User.LastName,
                Date = a.Date,
                Department = a.User != null && a.User.departments != null ? a.User.departments.Name : string.Empty,
                Status = a.Status
            }).ToListAsync();
            return result;
        }
    }
}
