using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Reports
{
    public class AttendanceReportService : IAttendanceReports
    {
        private readonly ApplicationDbContext db;
        public AttendanceReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> fetchTotalLeavesTaken()
        {
            var data = await db.leavebalances.SumAsync(lb => lb.TotalLeaves);
            return data;
        }

        public async Task<int> fetchTotalHolidaysTaken()
        {
            var data = await db.events.CountAsync();
            return data;
        }

        public async Task<IEnumerable<AttendanceReportViewModel>> getAttendancesAsync()
        {
            return await db.attendances.Include(a => a.User).Select(a => new AttendanceReportViewModel
            {
                AttendanceId = a.AttendanceId,
                AttendanceName = a.User.FirstName + " " + a.User.LastName,
                Date= a.Date,
                profilephoto = a.User.ProfilePicture,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut,
                Late= a.Late,
                LunchIn= a.LunchIn,
                LunchOut= a.LunchOut,
                BreakHours= a.BreakHours,
                WorkingHours= a.WorkingHours,
                ProductionHours= a.ProductionHours,
                OvertimeHours= a.OvertimeHours,
            }).ToListAsync();
        }

        public async Task<IEnumerable<AttendanceReportViewModel>> sortAttendances(string? datefilter, string? statusfilter, string? sortType)
        {
            var query = db.attendances.Include(a => a.User).AsQueryable();

            if (!string.IsNullOrEmpty(statusfilter))
            {
                query = query.Where(a => a.Status == statusfilter);
            }

            if (sortType == "Ascending")
            {
                query = query.OrderBy(a=> a.Date);
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
            if (datefilter == "Last 7 Days")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddDays(-7));
            }
            else if (datefilter == "Last 30 Days")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddDays(-30));
            }
            else if (datefilter == "This Year")
            {
                query = query.Where(a => a.Date.Year == DateTime.Now.Year);
            }
            else if (datefilter == "Last Year")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddYears(-1));
            }
            else if (datefilter == "Yesterday")
            {
                query = query.Where(a => a.Date >= DateTime.Now.AddDays(-1));
            }

            var result = await query.Select(av => new AttendanceReportViewModel
            {
                AttendanceId = av.AttendanceId,
                profilephoto = av.User.ProfilePicture,
                AttendanceName = av.User.FirstName + " " + av.User.LastName,
                Date = av.Date,
                CheckIn = av.CheckIn,
                CheckOut = a.CheckOut,
                Late = av.Late,
                LunchIn = av.LunchIn,
                LunchOut = av.LunchOut,
                BreakHours = av.BreakHours,
                WorkingHours = av.WorkingHours,
                ProductionHours = av.ProductionHours,
                OvertimeHours = av.OvertimeHours,
            }).ToListAsync();

            return result;

        }
    }
}
