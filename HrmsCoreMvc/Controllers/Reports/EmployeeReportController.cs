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

            if (!string.IsNullOrEmpty(sortType) || !string.IsNullOrEmpty(sortStatus))
            {
                empsLists = await er.SortEmployeeReportsAsync(sortType, sortStatus);
            }

            ViewBag.DeptCount = deptCount;
            ViewBag.EmpCount = empCount;
            ViewBag.ActiveEmpCount = activeEmpCount;
            ViewBag.ActiveRoles = activeRoles;
            return View("~/Views/Reports/EmployeeRepView.cshtml", empsLists);
        }
    }
}
