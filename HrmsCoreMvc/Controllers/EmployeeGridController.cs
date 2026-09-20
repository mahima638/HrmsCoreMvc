using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Controllers
{
    public class EmployeeGridController : Controller
    {
        private readonly ApplicationDbContext db;

        public EmployeeGridController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = await db.user.ToListAsync();
            var allProjectsUsers = await db.projectsUsers.ToListAsync();              
            var allTaskMembers = await db.taskmembers.ToListAsync();      
            var allDesignations = await db.designation.ToListAsync();     
            var allTasks = await db.tasks.ToListAsync();

            int totalEmployees = allUsers.Count;
            int activeEmployees = allUsers.Count(u => u.Status == "Active");
            int inactiveEmployees = allUsers.Count(u => u.Status == "Inactive");

            DateTime today = DateTime.Today;
            DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
            int newJoiners = allUsers.Count(u => u.DateOfJoining.HasValue
                                               && u.DateOfJoining.Value >= startOfMonth
                                               && u.DateOfJoining.Value <= today);

            List<EmployeeGridItem> employeeGridList = new List<EmployeeGridItem>();

            foreach (var u in allUsers)
            {
                var matchedDesignation = allDesignations.FirstOrDefault(d => d.DesignationId == u.DesignationId);
                string desigName = matchedDesignation != null ? matchedDesignation.Name : "Not Assigned";

                int projectsCount = allProjectsUsers.Count(pu => pu.UsersUserId == u.UserId);

                var myTaskMembers = allTaskMembers.Where(tm => tm.UserId == u.UserId).ToList();
                int totalAssignedTasks = myTaskMembers.Count;

                int doneCount = 0;
                int progressCount = 0;

                foreach (var tm in myTaskMembers)
                {
                    var matchedTask = allTasks.FirstOrDefault(t => t.TaskId == tm.TaskId);

                    if (matchedTask != null)
                    {
                        if (matchedTask.Status == "Completed")
                        {
                            doneCount = doneCount + 1;
                        }
                        else if (matchedTask.Status == "In Progress")
                        {
                            progressCount = progressCount + 1;
                        }
                    }
                }

                decimal productivity = 0;
                if (totalAssignedTasks > 0)
                {
                    productivity = ((decimal)doneCount / totalAssignedTasks) * 100;
                }

                var item = new EmployeeGridItem();
                item.FullName = u.FirstName + " " + u.LastName;
                item.ProfilePicture = u.ProfilePicture;
                item.DesignationName = desigName;
                item.ProjectsCount = projectsCount;
                item.DoneCount = doneCount;
                item.ProgressCount = progressCount;
                item.ProductivityPercent = productivity;
                employeeGridList.Add(item);
            }

            var vm = new EmployeeGridViewModel();
            vm.TotalEmployees = totalEmployees;
            vm.ActiveEmployees = activeEmployees;
            vm.InactiveEmployees = inactiveEmployees;
            vm.NewJoiners = newJoiners;
            vm.EmployeeGridList = employeeGridList;

            return View("EmployeeGridView", vm);
        }
    }
}