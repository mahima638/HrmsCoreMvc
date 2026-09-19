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
            var chartData = await lr.fetchChart();

            if (!string.IsNullOrEmpty(datefilter) || !string.IsNullOrEmpty(statusfilter) || !string.IsNullOrEmpty(sortType))
            {
                fetchleaves = await lr.sortLeaves(datefilter, statusfilter, sortType);
            }

            ViewBag.pendingLeaves = pendindLeaves;
            ViewBag.chartLabels = chartData.Labels;
            ViewBag.paidLeaves = chartData.PaidLeaves;
            ViewBag.approvedLeaves = approvedLeaves;
            ViewBag.rejectedLeaves = rejectedLeaves;
            ViewBag.totalLeaves = totalLeaves;
            ViewBag.SelectedDatefilter = datefilter;
            ViewBag.selectedStatusfilter = statusfilter;
            ViewBag.selectedsortType = sortType;
            return View("~/Views/Reports/GetLeavesReports.cshtml",fetchleaves);

        }
    }
}
