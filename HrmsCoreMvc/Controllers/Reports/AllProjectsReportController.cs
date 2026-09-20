using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class AllProjectsReportController : Controller
    {
        IAllProjectsService ap;

        public AllProjectsReportController(IAllProjectsService ap)
        {
            this.ap = ap;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjectsReports(string? priorityType, string? statusType, string? sortType)
        {
            var allprojects = await ap.fetchAllProjects();
            var onholdprojects = await ap.fetchOnHoldProjects();
            var overdueprojects = await ap.fetchOverdueProjects();
            var projectreports = await ap.fetchProjectReports();
            var chartData = await ap.fetchCharts();

            if (!string.IsNullOrEmpty(priorityType) || !string.IsNullOrEmpty(statusType) || !string.IsNullOrEmpty(sortType))
            {
                projectreports = await ap.sortProjectReports(priorityType, statusType, sortType);
            }
            ViewBag.SelectedPriorityType = priorityType;
            ViewBag.StatusType = statusType;
            ViewBag.SortType = sortType;
            ViewBag.ChartLabels = chartData.Chartlabels;
            ViewBag.InActiveProjects = chartData.InActiveProjects;
            ViewBag.ActiveProjects = chartData.ActiveProjects;
            ViewBag.InProgressTasks = chartData.InProgressTasks;
            ViewBag.CompletedTasks = chartData?.CompletedTasks;
            ViewBag.allprojects = allprojects;
            ViewBag.onholdprojects = onholdprojects;
            ViewBag.overdueprojects = overdueprojects;
            return View("~/Views/Reports/GetAllProjectsReports.cshtml",projectreports);
        }
    }
}
