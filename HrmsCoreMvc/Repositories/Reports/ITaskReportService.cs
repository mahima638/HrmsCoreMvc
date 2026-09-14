using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface ITaskReportService
    {
        Task<int> fetchTotalTasks();

        Task<int> fetchCompletedTasks();

        Task<int> fetchOnHoldTasks();

        Task<int> fetchOverdueTasks();

        Task<IEnumerable<TaskReportViewModel>> fetchTasks();
    }
}
