using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class DailyReportsController : Controller
    {
        IDailyReportService Ir;

        public DailyReportsController(IDailyReportService Ir)
        {
            this.Ir = Ir;
        }

        [HttpGet]
        public async Task<IActionResult> fetchDailyReports(string? status, string? sortType)
        {
            var TotalAbsent = await Ir.fetchTotalAbsent();
            var TotalPresent = await Ir.fetchTotalPresent();
            var CompletedTasks = await Ir.fetchCompletedTasks();
            var PendingTasks = await Ir.fetchPendingTasks();
            var DailyTasks = await Ir.fetchDailyTasks();
            var dailyChart = await Ir.getDailyTasksChart();
            if(!string.IsNullOrEmpty(status) || !string.IsNullOrEmpty(sortType)) 
            {
                 DailyTasks = await Ir.sortDailyTasks(status, sortType);
            }
            ViewBag.ChartLabels = dailyChart.ChartLabels;
            ViewBag.ChartPresent = dailyChart.Chartpresent;
            ViewBag.ChartAbsent = dailyChart.Chartabsent;
            ViewBag.TotalAbsent = TotalAbsent;
            ViewBag.TotalPresent = TotalPresent;
            ViewBag.CompletedTasks = CompletedTasks;
            ViewBag.PendingTasks = PendingTasks;
            return View("~/Views/Reports/fetchDailyReports.cshtml",DailyTasks);

        }
    }
}
