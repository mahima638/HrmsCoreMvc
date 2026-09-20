using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface IAllProjectsService
    {
        Task<int> fetchAllProjects();

        Task<int> fetchOnHoldProjects();

        Task<int> fetchOverdueProjects();

        Task<IEnumerable<ProjectReportsViewModel>> fetchProjectReports();

        Task<IEnumerable<ProjectReportsViewModel>> sortProjectReports(string? priorityType, string? statusType, string? sortType);

        Task<ProjectChartDto> fetchCharts();
    }
}
