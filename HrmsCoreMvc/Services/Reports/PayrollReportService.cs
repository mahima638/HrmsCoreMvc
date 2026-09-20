using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.Reports;
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

        public async Task<List<PayslipViewModel>> fetchPayslips()
        {
            return await db.Payslips.Include(p => p.User).Select(p => new PayslipViewModel
            {
                PayslipID= p.PayslipId,
                Name= p.User.FirstName + " " + p.User.LastName,
                Month = p.Month,
                Year = p.Year
            }).ToListAsync();
        }

        public async Task<List<PayslipViewModel>> sortPayslips(string? sortMonth, string? sortType)
        {
            var query = db.Payslips.Include(p => p.User).AsQueryable();
            if (!string.IsNullOrEmpty(sortMonth))
            {
                if(sortMonth == "Jan-March")
                {
                    query = query.Where(p => new[] { "January", "Februrary", "March" }.Contains(p.Month));
                }
                else if (sortMonth == "April-Jun")
                {
                    query = query.Where(p => new[] { "April", "May", "June" }.Contains(p.Month));
                }
                else if (sortMonth == "July-Sept")
                {
                    query = query.Where(p => new[] { "July", "August", "September" }.Contains(p.Month));
                }
                else if(sortMonth == "Oct-Dec")
                {
                    query = query.Where(p => new[] { "October", "November", "December" }.Contains(p.Month));
                }

            }

            if(sortType == "Ascending")
            {
                query = query
                 .OrderBy(p => p.Year)
                .ThenBy(p => p.Month == "January" ? 1 :
                      p.Month == "February" ? 2 :
                      p.Month == "March" ? 3 :
                      p.Month == "April" ? 4 :
                      p.Month == "May" ? 5 :
                      p.Month == "June" ? 6 :
                      p.Month == "July" ? 7 :
                      p.Month == "August" ? 8 :
                      p.Month == "September" ? 9 :
                      p.Month == "October" ? 10 :
                      p.Month == "November" ? 11 : 12);
            }
            else if (sortType == "Descending")
            {
                query = query
                .OrderByDescending(p => p.Year)
                .ThenBy(p => p.Month == "January" ? 1 :
                     p.Month == "February" ? 2 :
                     p.Month == "March" ? 3 :
                     p.Month == "April" ? 4 :
                     p.Month == "May" ? 5 :
                     p.Month == "June" ? 6 :
                     p.Month == "July" ? 7 :
                     p.Month == "August" ? 8 :
                     p.Month == "September" ? 9 :
                     p.Month == "October" ? 10 :
                     p.Month == "November" ? 11 : 12);
            }

            var result = await query.Select(p => new PayslipViewModel
            {
                PayslipID = p.PayslipId,
                Name = p.User.FirstName + " " + p.User.LastName,
                Month = p.Month,
                Year = p.Year
            }).ToListAsync();

            return result;

        }
    }
}
