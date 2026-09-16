using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface IDailyReportService
    {
        Task <int> fetchTotalPresent();

        Task<int> fetchTotalAbsent();

        Task<int> fetchCompletedTasks();

        Task<int> fetchPendingTasks();

        Task<IEnumerable<DailyAttendanceViewModel>> fetchDailyTasks();

        Task<IEnumerable<DailyAttendanceViewModel>> sortDailyTasks(string? status, string? sortType);
    }
}
