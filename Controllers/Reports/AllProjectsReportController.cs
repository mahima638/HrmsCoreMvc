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
        public async Task<IActionResult> GetAllProjecrsReports(string? priorityType, string? statusType, string? sortType)
        {
            var allprojects = await ap.fetchAllProjects();
            var onholdprojects = await ap.fetchOnHoldProjects();
            var overdueprojects = await ap.fetchOverdueProjects();
            var projectreports = await ap.fetchProjectReports();

            if (!string.IsNullOrEmpty(priorityType) || !string.IsNullOrEmpty(statusType) || !string.IsNullOrEmpty(sortType))
            {
                projectreports = await ap.sortProjectReports(priorityType, statusType, sortType);
            }
            ViewBag.allprojects = allprojects;
            ViewBag.onholdprojects = onholdprojects;
            ViewBag.overdueprojects = overdueprojects;
            return View(projectreports);
        }
    }
}
