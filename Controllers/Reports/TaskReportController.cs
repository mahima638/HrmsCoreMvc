using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class TaskReportController : Controller
    {
        ITaskReportService t;

        public TaskReportController(ITaskReportService t)
        {
            this.t = t;
        }

        [HttpGet]
        public async Task<IActionResult> Getreports(string? priority, string? status, string? sortType)
        {
            var completedTasks = await t.fetchCompletedTasks();
            var onHoldTasks = await t.fetchOnHoldTasks();
            var overdueTasks = await t.fetchOverdueTasks();
            var fetchTasks = await t.fetchTasks();
            if (!string.IsNullOrEmpty(priority) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(sortType)) 
            {
                fetchTasks = await t.sortTasks(priority,status,sortType);
            }
            ViewBag.completedTasks = completedTasks;
            ViewBag.onHoldTasks = onHoldTasks;
            ViewBag.overdueTasks = overdueTasks;
            return View(fetchTasks);


        }
    }
}
