using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Attendance;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Models.ViewModel;
namespace HrmsCoreMvc.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext db;
        public AttendanceService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Attendance?> FetchAttendance(int userId)
        {
            var today = DateTime.Today;
            return await db.Attendance
                .FirstOrDefaultAsync(a => a.UserId == userId && a.Date == today);
        }
        public async Task CheckIn(int userId)
        {
            var attendance = await FetchAttendance(userId);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    UserId = userId,
                    Date = DateTime.Today,
                    CheckIn = DateTime.Now,
                    Status = "Present"
                };

                await db.Attendance.AddAsync(attendance);
                await db.SaveChangesAsync();
            }
        }

        public async Task LunchIn(int userId)
        {
            var attendance = await FetchAttendance(userId);

            if (attendance != null && attendance.CheckIn != null && attendance.LunchIn == null)
            {
                attendance.LunchIn = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task LunchOut(int userId)
        {
            var attendance = await FetchAttendance(userId);

            if (attendance != null && attendance.LunchIn != null && attendance.LunchOut == null)
            {
                attendance.LunchOut = DateTime.Now;

                attendance.BreakHours =
                    (decimal)(attendance.LunchOut.Value - attendance.LunchIn.Value).TotalHours;

                await db.SaveChangesAsync();
            }
        }

        public async Task CheckOut(int userId)
        {
            var attendance = await FetchAttendance(userId);

            if (attendance != null && attendance.CheckOut == null)
            {
                attendance.CheckOut = DateTime.Now;

                attendance.WorkingHours =
                    (decimal)(attendance.CheckOut.Value - attendance.CheckIn.Value).TotalHours
                    - attendance.BreakHours;

                await db.SaveChangesAsync();
            }
        }
        public async Task<AttendanceViewModel> GetDashboard(int userId)
        {
            var today = DateTime.Today;

            var todayAttendance = await FetchAttendance(userId);

            var history = await db.Attendance
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            var weekStart = today.AddDays(-(int)today.DayOfWeek);

            var monthStart = new DateTime(today.Year, today.Month, 1);

            var weekHours = await db.Attendance
                .Where(x => x.UserId == userId && x.Date >= weekStart)
                .SumAsync(x => (decimal?)x.WorkingHours) ?? 0;

            var monthHours = await db.Attendance
                .Where(x => x.UserId == userId && x.Date >= monthStart)
                .SumAsync(x => (decimal?)x.WorkingHours) ?? 0;

            var overtime = await db.Attendance
                .Where(x => x.UserId == userId && x.Date >= monthStart)
                .SumAsync(x => (decimal?)x.OvertimeHours) ?? 0;

            return new AttendanceViewModel
            {
                TodayAttendance = todayAttendance,
                AttendanceHistory = history,

                EmployeeName = "Ram",          // Replace later with logged-in user

                ProfilePicture = "avatar.jpg", // Replace later

                TotalHoursToday = todayAttendance?.WorkingHours ?? 0,

                TotalHoursWeek = weekHours,

                TotalHoursMonth = monthHours,

                ProductionHours = todayAttendance?.ProductionHours ?? 0,

                BreakHours = todayAttendance?.BreakHours ?? 0,

                OvertimeHours = overtime
            };
        }


        public async Task<AttendanceViewModel> GetAdminAttendanceDashboard()
        {
            var today = DateTime.Today;

            var attendanceToday = await db.Attendance
                .Include(a => a.User)
                .Where(a => a.Date.Date == today)
                .ToListAsync();

            return new AttendanceViewModel
            {
                TotalEmployees = await db.user.CountAsync(),

                PresentCount = attendanceToday.Count(a => a.Status == "Present"),
                LateLoginCount = attendanceToday.Count(a => a.Late > 0),
                UninformedCount = attendanceToday.Count(a => a.Status == "Uninformed"),
                PermissionCount = attendanceToday.Count(a => a.Status == "Permission"),
                AbsentCount = attendanceToday.Count(a => a.Status == "Absent"),
                AttendanceHistory = attendanceToday
            };
        }

    }
}
