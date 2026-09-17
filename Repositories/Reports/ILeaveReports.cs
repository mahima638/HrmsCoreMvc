using HrmsCoreMvc.Models.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface ILeaveReports
    {
        Task<int> fetchTotalLeaves();

        Task<int> fetchApprovedLeaves();

        Task<int> fetchPendingLeaves();

        Task<int> fetchRejectedLeaves();

        Task<IEnumerable<LeavesReportViewModel>> fetchLeaves();

        Task<IEnumerable<LeavesReportViewModel>> sortLeaves(string? datefilter, string? statusfilter, string? sortType);
    }
}
