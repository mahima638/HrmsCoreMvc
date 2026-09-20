using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
   public interface IAttendanceReports
    {
        Task<int> fetchTotalLeavesTaken();

        Task<int> fetchTotalHolidaysTaken();

        Task<int> fetchTotalHalfDays();

        Task<int> fetchTotalWorkingDays();

        Task<IEnumerable<AttendanceReportViewModel>> getAttendancesAsync();

        Task<IEnumerable<AttendanceReportViewModel>> sortAttendances(string? datefilter, string? statusfilter, string? sortType);

        Task<AttendanceChartDto> GetAttendanceChartDataAsync();

    }
}
