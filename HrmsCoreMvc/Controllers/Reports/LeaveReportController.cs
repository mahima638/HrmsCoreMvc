using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class LeaveReportController : Controller
    {
        ILeaveReports lr;
        public LeaveReportController(ILeaveReports lr)
        {
            this.lr = lr;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetLeavesReports(string? datefilter, string? statusfilter, string? sortType)
        {
            var approvedLeaves = await lr.fetchApprovedLeaves();
            var pendindLeaves = await lr.fetchPendingLeaves();
            var rejectedLeaves = await lr.fetchRejectedLeaves();
            var totalLeaves = await lr.fetchTotalLeaves();
            var fetchleaves = await lr.fetchLeaves();

            if (!string.IsNullOrEmpty(datefilter) && !string.IsNullOrEmpty(statusfilter) && !string.IsNullOrEmpty(sortType))
            {
                fetchleaves = await lr.sortLeaves(datefilter, statusfilter, sortType);
            }

            ViewBag.approvedLeaves = approvedLeaves;
            ViewBag.rejectedLeaves = rejectedLeaves;
            ViewBag.totalLeaves = totalLeaves;
            return View(fetchleaves);

        }
    }
}
