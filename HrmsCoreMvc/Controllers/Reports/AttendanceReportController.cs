using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class AttendanceReportController : Controller
    {
        IAttendanceReports a;
        public AttendanceReportController(IAttendanceReports a)
        {
            this.a = a;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendanceReports(string? datefilter, string? statusfilter, string? sortType)
        {
            var fetchTotalLeaves = await a.fetchTotalLeavesTaken();
            var fetchTotalHolidays = await a.fetchTotalHolidaysTaken();
            var fetchattendances = await a.getAttendancesAsync();
            var chartData = await a.GetAttendanceChartDataAsync();
            var fetchTotalHalfDays = await a.fetchTotalHalfDays();
            var fetchTotalWorkingDays = await a.fetchTotalWorkingDays();
            if(!string.IsNullOrEmpty(datefilter) || !string.IsNullOrEmpty(statusfilter) || !string.IsNullOrEmpty(sortType))
            {
                fetchattendances = await a.sortAttendances(datefilter, statusfilter, sortType);
            }
            ViewBag.TotalWorkingDays = fetchTotalWorkingDays;
            ViewBag.TotalHalfDays = fetchTotalHalfDays;
            ViewBag.ChartLabel = chartData.Labels;
            ViewBag.ChartPresent = chartData.PresentData;
            ViewBag.ChartAbsent = chartData.AbsentData;
            ViewBag.totalLeaves= fetchTotalLeaves;
            ViewBag.totalHolidays= fetchTotalHolidays;
            ViewBag.SelectedDatefilter = datefilter;
            ViewBag.selectedStatusfilter = statusfilter;
            ViewBag.selectedsortType = sortType;
            return View("~/Views/Reports/GetAttendanceReports.cshtml",fetchattendances);
        }
    }
}
