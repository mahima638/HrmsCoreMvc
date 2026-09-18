using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class EmployeeReportController : Controller
    {
        IEmployeeReportService er;

        public EmployeeReportController(IEmployeeReportService er)
        {
            this.er = er;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeRepView(string? sortType, string? sortStatus)
        {
            var empCount = await er.FetchEmpCount();
            var activeEmpCount = await er.FetchActiveEmployee();
            var activeRoles = await er.FetchActiveRoles();
            var deptCount = await er.FetchDepartments();

            var empsLists = await er.GetEmployeeReportsAsync();

            var chartData = await er.GetEmployeeChartDataAsync();

            if (!string.IsNullOrEmpty(sortType) || !string.IsNullOrEmpty(sortStatus))
            {
                empsLists = await er.SortEmployeeReportsAsync(sortType, sortStatus);
            }

            ViewBag.ChartLabels = chartData.Labels;
            ViewBag.ChartActive = chartData.ActiveData;
            ViewBag.ChartInactive = chartData.InactiveData;
            ViewBag.DeptCount = deptCount;
            ViewBag.EmpCount = empCount;
            ViewBag.ActiveEmpCount = activeEmpCount;
            ViewBag.ActiveRoles = activeRoles;

            ViewBag.SelectedSortType = sortType;
            ViewBag.SelectedSortStatus = sortStatus;

            return View("~/Views/Reports/EmployeeRepView.cshtml", empsLists);
        }
    }
}
