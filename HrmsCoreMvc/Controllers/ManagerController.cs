using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HrmsCoreMvc.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext db;
        public ManagerController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public  async Task<IActionResult> Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                
                return RedirectToAction("Login", "Account");
            }
            var allUsers = await db.user.ToListAsync();

            User employee = null;

            foreach (var user in allUsers) {

                if (user.UserId == userId) {

                    employee = user;
                }
            }
            if (employee == null) {

                return NotFound();
            }

            var allBalances = await db.LeaveBalances.ToListAsync();
            var totalLeaves = 0;
            var taken = 0;

            foreach (var leave in allBalances) {

                if (leave.UserId == userId)
                 {
                    totalLeaves = totalLeaves + leave.TotalLeaves;
                    taken = taken + leave.UsedLeaves;
                
                }
            }

            var allAttendance = await db.Attendance.ToListAsync();
            int onTime = 0;
            int lateAttendance = 0;
            int absent = 0;
            int workDays = 0;

            foreach (var at in allAttendance) {

                if (at.UserId == userId) {

                    if (at.Status == "Present" && at.Late == 0) {

                        onTime = onTime + 1;
                    }
                    if (at.Status == "Present" && at.Late > 0) {

                        lateAttendance = lateAttendance + 0;
                    }
                    if (at.Status == "Absent") {
                        absent = absent + 1;

                    }
                    if (at.CheckIn != null) {

                        workDays = workDays + 1;
                    }
                 
                }
            
            }
            DateTime today = DateTime.Today;
            DateTime StartOfWeek = today.AddDays(-(int)today.DayOfWeek);
            DateTime StartOfMonth = new DateTime(today.Year, today.Month, 1);

          decimal hoursToday = 0;
           decimal hoursThisWeek = 0;

            decimal hoursThisMonth = 0;
            decimal overTimeThisMonth = 0;

            bool IsCheckedIn = false;
            DateTime? todayCheckInTime = null;
            decimal todayProductionHours = 0;

            foreach (var at in allAttendance)
            {
                if (at.UserId == userId) {

                    if (at.Date >= StartOfMonth && at.Date <= today) {

                        hoursThisMonth = hoursThisMonth + at.WorkingHours;
                        overTimeThisMonth = overTimeThisMonth + at.OvertimeHours;

                    }
                    if (at.Date >= StartOfWeek && at.Date <= today) {
                        hoursThisWeek = hoursThisWeek + at.WorkingHours;
                      
                    }
                    if (at.Date == today) {
                        hoursToday = hoursToday + at.WorkingHours;
                        todayProductionHours = todayProductionHours + at.ProductionHours;

                        if (at.CheckIn != null) {
                            IsCheckedIn = true;
                            todayCheckInTime = at.CheckIn;
                        }
                    }
                }


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
            obj.OvertimeThisMonth = overTimeThisMonth;
            obj.IsCheckedIn = IsCheckedIn;
            obj.TodayCheckInTime = todayCheckInTime;
            obj.TodayProductionHours = todayProductionHours;



            return View(obj);
        }

       
            
            

    }
}
