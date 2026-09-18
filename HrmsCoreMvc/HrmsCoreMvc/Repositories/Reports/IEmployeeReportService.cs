using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface IEmployeeReportService
    {
        Task<int> FetchEmpCount();

        Task<int> FetchActiveEmployee();

        Task<int> FetchActiveRoles();

        Task<int> FetchDepartments();

        Task<IEnumerable<EmployeeReportViewModel>> GetEmployeeReportsAsync();

        Task<IEnumerable<EmployeeReportViewModel>> SortEmployeeReportsAsync(string? sortType, string? sortStatus);

        Task<EmployeeChartDto> GetEmployeeChartDataAsync();
    }
}
