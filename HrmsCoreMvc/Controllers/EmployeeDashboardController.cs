using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        private readonly ApplicationDbContext db;

        public EmployeeDashboardController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var allUsers = await db.user.ToListAsync();

            User employee = allUsers.FirstOrDefault(user => user.UserId == userId.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var allBalances = await db.LeaveBalances.ToListAsync();

            var userBalances = allBalances.Where(leave =>
                leave.UserId == userId.Value);

            int totalLeaves = userBalances.Sum(leave => leave.TotalLeaves);
            int taken = userBalances.Sum(leave => leave.UsedLeaves);

            var allAttendance = await db.Attendance.ToListAsync();

            var userAttendance = allAttendance.Where(at =>
                at.UserId == userId.Value);

            int onTime = userAttendance.Count(at =>
                at.Status == "Present" && at.Late == 0);

            int lateAttendance = userAttendance.Count(at =>
                at.Status == "Present" && at.Late > 0);

            int absent = userAttendance.Count(at =>
                at.Status == "Absent");

            int workDays = userAttendance.Count(at =>
                at.CheckIn != null);

            DateTime today = DateTime.Today;

            DateTime startOfWeek =
                today.AddDays(-(int)today.DayOfWeek);

            DateTime startOfMonth =
                new DateTime(today.Year, today.Month, 1);

            var monthAttendance = userAttendance.Where(at =>
                at.Date >= startOfMonth &&
                at.Date <= today);

            var weekAttendance = userAttendance.Where(at =>
                at.Date >= startOfWeek &&
                at.Date <= today);

            var todayAttendance = userAttendance.Where(at =>
                at.Date.Date == today);

            decimal hoursThisMonth =
                monthAttendance.Sum(at => at.WorkingHours);

            decimal overtimeThisMonth =
                monthAttendance.Sum(at => at.OvertimeHours);

            decimal hoursThisWeek =
                weekAttendance.Sum(at => at.WorkingHours);

            decimal hoursToday =
                todayAttendance.Sum(at => at.WorkingHours);

            decimal todayProductionHours =
                todayAttendance.Sum(at => at.ProductionHours);

            var todayCheckIn = todayAttendance.FirstOrDefault(at =>
                at.CheckIn != null);

            bool isCheckedIn = todayCheckIn != null;

            DateTime? todayCheckInTime = null;

            if (todayCheckIn != null)
            {
                todayCheckInTime = todayCheckIn.CheckIn;
            }

            var obj = new EmployeeDashboardViewModel();

            obj.Employee = employee;
            obj.TotalLeaves = totalLeaves;
            obj.Taken = taken;
            obj.SickLeave = 0;
            obj.OnTime = onTime;
            obj.LateAttendance = lateAttendance;
            obj.Absent = absent;
            obj.WorkedDays = workDays;
            obj.LossOfPay = 0;
            obj.HoursToday = hoursToday;
            obj.HoursThisWeek = hoursThisWeek;
            obj.HoursThisMonth = hoursThisMonth;
            obj.OvertimeThisMonth = overtimeThisMonth;
            obj.IsCheckedIn = isCheckedIn;
            obj.TodayCheckInTime = todayCheckInTime;
            obj.TodayProductionHours = todayProductionHours;

            return View("~/Views/Shared/EmployeeDashboard.cshtml", obj);
        }
    }
}