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
        public async Task<IActionResult> GetTaskreports(string? priority, string? status, string? sortType)
        {
            var completedTasks = await t.fetchCompletedTasks();
            var onHoldTasks = await t.fetchOnHoldTasks();
            var overdueTasks = await t.fetchOverdueTasks();
            var totalTasks = await t.fetchTotalTasks();
            var fetchTasks = await t.fetchTasks();
            var chartData = await t.fetchCharts();
            if (!string.IsNullOrEmpty(priority) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(sortType)) 
            {
                fetchTasks = await t.sortTasks(priority,status,sortType);
            }
            ViewBag.SelectedPriorityType = priority;
            ViewBag.SelecetedStatusType = status;
            ViewBag.SelectedPriorityType = sortType;
            ViewBag.Chartlabels = chartData.ChartLabels;
            ViewBag.ChartCompleted = chartData.ChartCompleted;
            ViewBag.ChartInprogress = chartData.ChartInProgress;
            ViewBag.ChartPending = chartData.ChartPending;
            ViewBag.ChartOnhold = chartData.ChartOnHold;
            ViewBag.totalTasks = totalTasks;
            ViewBag.completedTasks = completedTasks;
            ViewBag.onHoldTasks = onHoldTasks;
            ViewBag.overdueTasks = overdueTasks;


            return View("~/Views/Reports/GetTaskreports.cshtml",fetchTasks);


        }
    }
}
