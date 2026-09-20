using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            var data = await db.LeaveBalances.SumAsync(lb => lb.TotalLeaves);
            return data;
        }

        public async Task<int> fetchTotalHolidaysTaken()
        {
            var data = await db.events.CountAsync();
            return data;
        }

        public async Task<IEnumerable<AttendanceReportViewModel>> getAttendancesAsync()
        {
            return await db.Attendance.Include(a => a.User).Select(a => new AttendanceReportViewModel
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
                Status = a.Status
            }).ToListAsync();
        }

        public async Task<IEnumerable<AttendanceReportViewModel>> sortAttendances(string? datefilter, string? statusfilter, string? sortType)
        {
            var query = db.Attendance.Include(a => a.User).AsQueryable();

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
                query = query.Where(a => a.Date.Year == DateTime.Now.Year - 1);
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
                CheckOut = av.CheckOut,
                Late = av.Late,
                LunchIn = av.LunchIn,
                LunchOut = av.LunchOut,
                BreakHours = av.BreakHours,
                WorkingHours = av.WorkingHours,
                ProductionHours = av.ProductionHours,
                OvertimeHours = av.OvertimeHours,
                Status = av.Status
            }).ToListAsync();

            return result;

        }

        public async Task<AttendanceChartDto> GetAttendanceChartDataAsync()
        {
            var groupData = await db.Attendance.Where(x => x.Date !=null && x.Status != null)
                .GroupBy(x => new
                {
                    Year = x.Date.Year,
                    Month = x.Date.Month
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Present = g.Count(x => x.Status == "Present"),
                    Absent = g.Count(x => x.Status == "Absent")

                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            var result = new AttendanceChartDto();
            foreach(var item in groupData)
            {
                result.Labels.Add($"{item.Year}-{item.Month:D2}");
                result.AbsentData.Add(item.Absent);
                result.PresentData.Add(item.Present);

            }

            return result;

        }

        public async Task<int> fetchTotalHalfDays()
        {
            var data = await db.Attendance.Where(x => x.Status == "Half Day").CountAsync();
            return data;
        }

        public async Task<int> fetchTotalWorkingDays()
        {
            return await db.Attendance.CountAsync();


        }
    }
}
