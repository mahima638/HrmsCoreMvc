using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Reports
{
    public class PayrollReportService : IPayslipService
    {
        private readonly ApplicationDbContext db;

        public PayrollReportService(ApplicationDbContext db)
        {
            this.db = db;
            
        }

        public Task<double> TotalPayroll 
        {
            var data = db.
        }
    }
}
