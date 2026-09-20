using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace HrmsCoreMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext db)
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

           
            User admin = allUsers.FirstOrDefault(u => u.UserId == userId.Value);

            if (admin == null)
            {
                return NotFound();
            }

          
            int totalEmployees = allUsers.Count();

            DateTime today = DateTime.Today;
            DateTime StartOfMonth = new DateTime(today.Year, today.Month, 1);

            int newHireThisMonth = allUsers.Count(u =>
                u.DateOfJoining.HasValue &&
                u.DateOfJoining >= StartOfMonth &&
                u.DateOfJoining <= today
            );

            var allAttendance = await db.Attendance.ToListAsync();

            int presentToday = allAttendance.Count(at =>
                at.Date.Date == today &&
                at.Status == "Present"
            );

          
            var allProjects = await db.AllProjects.ToListAsync();
            var allTasks = await db.tasks.ToListAsync();

          
            int totalProjects = allProjects.Count();

           
            List<string> uniqueClients = allProjects
                .Where(p => !string.IsNullOrEmpty(p.ClientName))
                .Select(p => p.ClientName)
                .Distinct()
                .ToList();

            int totalClients = uniqueClients.Count();

            int totalTasks = allTasks.Count();

           
            var allDepartments = await db.department.ToListAsync();

            List<DepartmentCount> departmentCounts = new List<DepartmentCount>();

            foreach (var d in allDepartments)
            {
                int countInDepartment = allUsers.Count(u =>
                    u.DepartmentId == d.DepartmentId
                );

                if (countInDepartment > 0)
                {
                    var item = new DepartmentCount();
                    item.DepartmentName = d.Name;
                    item.Count = countInDepartment;

                    departmentCounts.Add(item);
                }
            }

         
            List<ClockInEntry> todayClockIns = new List<ClockInEntry>();

            var todayAttendance = allAttendance.Where(at =>
                at.Date.Date == today &&
                at.CheckIn != null
            );

            foreach (var at in todayAttendance)
            {
                User matchedUser = allUsers.FirstOrDefault(u =>
                    u.UserId == at.UserId
                );

                if (matchedUser != null)
                {
                    var entry = new ClockInEntry();

                    entry.FullName = matchedUser.FirstName + " " + matchedUser.LastName;
                    entry.DesignationName = matchedUser.ProfilePicture;
                    entry.CheckInTime = at.CheckIn.Value.ToString("hh:mm tt");

                    if (at.CheckOut != null)
                    {
                        entry.CheckOutTime = at.CheckOut.Value.ToString("hh:mm tt");
                    }
                    else
                    {
                        entry.CheckOutTime = "Not Checked out!";
                    }

                    entry.ProductionHoursDisplay =
                        at.ProductionHours.ToString("0.00") + "Hrs";

                    if (at.Late > 0)
                    {
                        entry.IsLate = true;
                        entry.LateMinutes = at.Late;
                    }
                    else
                    {
                        entry.IsLate = false;
                    }

                    todayClockIns.Add(entry);
                }
            }

            List<AllProjects> recentProjects = allProjects;

            
            List<string> uniqueTaskStatuses = allTasks
                .Where(t => !string.IsNullOrEmpty(t.Status))
                .Select(t => t.Status)
                .Distinct()
                .ToList();

            List<StatusCount> taskStatusBreakdown = new List<StatusCount>();

            foreach (var status in uniqueTaskStatuses)
            {
                int countForThisStatus = allTasks.Count(t =>
                    t.Status == status
                );

                var sc = new StatusCount();

                sc.StatusName = status;
                sc.Count = countForThisStatus;

                taskStatusBreakdown.Add(sc);
            }

         
            int totalTasksCompleted = allTasks.Count(t =>
                t.Status == "Completed"
            );

          
            List<EmployeeListItem> employeeList = new List<EmployeeListItem>();

            foreach (var u in allUsers)
            {
                string deptName = "Not Assigned";

                var department = allDepartments.FirstOrDefault(d =>
                    d.DepartmentId == u.DepartmentId
                );

                if (department != null)
                {
                    deptName = department.Name;
                }

                var item = new EmployeeListItem();

                item.FullName = u.FirstName + " " + u.LastName;
                item.ProfilePicture = u.ProfilePicture;
                item.DepartmentName = deptName;

                employeeList.Add(item);
            }

         
            var obj = new AdminDashboardViewModel();

            obj.Admin = admin;
            obj.TotalEmployees = totalEmployees;
            obj.NewHireThisMonth = newHireThisMonth;
            obj.PresentToday = presentToday;
            obj.TotalProjects = totalProjects;
            obj.TotalClients = totalClients;
            obj.TotalTasks = totalTasks;
            obj.DepartmentCounts = departmentCounts;
            obj.TodayClockIns = todayClockIns;
            obj.RecentProjects = recentProjects;
            obj.TaskStatusBreakdown = taskStatusBreakdown;
            obj.TotalTasksCompleted = totalTasksCompleted;
            obj.AllEmployeesList = employeeList;

            return View(obj);
        }
    }
}
