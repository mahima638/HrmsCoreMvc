using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
            
        }
        public  async Task<IActionResult> Dashboard()
        {

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Login", "Account");
            }

            var allUsers = await db.user.ToListAsync();

            User admin = null;
            foreach (var user in allUsers)
            {

                if (user.UserId == userId.Value)
                {

                    admin = user;
                }
            }

                if (admin == null)
                {
                    return NotFound();
                }

                int totalEmployees = 0;
                foreach (var u in allUsers) {

                    totalEmployees = totalEmployees + 1;
                }

                DateTime today = DateTime.Today;
                DateTime StartOfMonth = new DateTime(today.Year, today.Month, 1);

                int newHireThisMonth = 0;

                foreach (var u in allUsers) {

                    if (u.DateOfJoining.HasValue && u.DateOfJoining >= StartOfMonth && u.DateOfJoining <= today) {

                        newHireThisMonth = newHireThisMonth + 1;
                    
                    }
                }
                var allAttendance = await db.Attendance.ToListAsync();

                int presentToday = 0;

                foreach (var at in allAttendance) {

                    if (at.Date.Date == today && at.Status == "Present")
                    {
                        presentToday = presentToday + 1;
                    }

                }

                var allProjects = await db.AllProjects.ToListAsync();
                var allTasks = await db.tasks.ToListAsync();

                int totalProjects = 0;
                foreach (var p in allProjects) {
                    totalProjects = totalProjects + 1;
                }

                List<string> uniqueClients = new List<string>();
                foreach (var p in allProjects) {

                    if (!string.IsNullOrEmpty(p.ClientName) && !uniqueClients.Contains(p.ClientName)) {
                        uniqueClients.Add(p.ClientName);
                    }
                }

                int totalClients = uniqueClients.Count;
                int totalTasks = 0;
                foreach (var t in allTasks)
                {
                    totalTasks = totalTasks + 1;
                }

            var allDepartments = await db.department.ToListAsync();
            List<DepartmentCount> departmentCounts = new List<DepartmentCount>();
            foreach (var d in allDepartments) {
                int countInDepartment = 0;
                foreach (var u in allUsers) {
                    if (u.DepartmentId == d.DepartmentId) {

                        countInDepartment = countInDepartment + 1;
                     
                    }
                  }
                if (countInDepartment > 0) {

                    var item = new DepartmentCount();
                    item.DepartmentName = d.Name;
                    item.Count = countInDepartment;
                    departmentCounts.Add(item);
                }
             
            }
            List<ClockInEntry> todayClockIns = new List<ClockInEntry>();

            foreach (var at in allAttendance)
            {
                if (at.Date.Date == today && at.CheckIn != null)
                {

                    User matchedUser = null;
                    foreach (var u in allUsers)
                    {

                        if (u.UserId == at.UserId)
                        {

                            matchedUser = u;
                        }
                    }
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
                        entry.ProductionHoursDisplay = at.ProductionHours.ToString("0.00") + "Hrs";

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
            }

                    
                    List<AllProjects> recentProjects = allProjects;
                    List<string> uniqueTaskStatuses = new List<string>();
                    foreach (var t in allTasks)
                    {
                        if (!string.IsNullOrEmpty(t.Status) && !uniqueTaskStatuses.Contains(t.Status))
                        {
                            uniqueTaskStatuses.Add(t.Status);
                        }
                    }

                    List<StatusCount> taskStatusBreakdown = new List<StatusCount>();
                    foreach (var status in uniqueTaskStatuses)
                    {
                        int countForThisStatus = 0;
                        foreach (var t in allTasks)
                        {
                            if (t.Status == status)
                            {
                                countForThisStatus = countForThisStatus + 1;
                            }
                        }

                        var sc = new StatusCount();
                        sc.StatusName = status;
                        sc.Count = countForThisStatus;
                        taskStatusBreakdown.Add(sc);
                    }

                  
                    int totalTasksCompleted = 0;
                    foreach (var t in allTasks)
                    {
                        if (t.Status == "Completed")
                        {
                            totalTasksCompleted = totalTasksCompleted + 1;
                        }
                    }
          
            List<EmployeeListItem> employeeList = new List<EmployeeListItem>();

            foreach (var u in allUsers)
            {
                string deptName = "Not Assigned";

                foreach (var d in allDepartments)
                {
                    if (d.DepartmentId == u.DepartmentId)
                    {
                        deptName = d.Name;
                    }
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


   
