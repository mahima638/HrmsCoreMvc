using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Reports
{
    public class PayrollReportController : Controller
    {
        IPayslipService ps;

        public PayrollReportController(IPayslipService ps)
        {
            this.ps = ps;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollReports(string? sortMonth, string? sortType)
        {
            var fetchPayslips = await ps.fetchPayslips();

            if( !string.IsNullOrEmpty(sortMonth) ||  !string.IsNullOrEmpty(sortType))
            {
                fetchPayslips = await ps.sortPayslips(sortMonth, sortType);
            }
            ViewBag.SelectedSortStatus = sortMonth;
            ViewBag.SelectedSortType = sortType;

            return View("Views/Reports/GetPayrollReports.cshtml", fetchPayslips);
        }

    }
}
