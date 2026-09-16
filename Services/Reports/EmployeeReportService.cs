using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Reports
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly ApplicationDbContext db;

        public EmployeeReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> FetchActiveEmployee()
        {
            var data = await db.user.Include(x => x.Role).Where(x => x.Role.RoleName.Contains("Employee") && x.Status == "Active").CountAsync();
            return data;
        }

        public async Task<int> FetchActiveRoles()
        {
            var data = await db.role.Select(x => x.RoleName).Distinct().CountAsync();
            return data;
        }

        public async Task<int> FetchDepartments()
        {
            var data = await db.department.Select(x => x.Name).Distinct().CountAsync();
            return data;
        }

        public async Task<int> FetchEmpCount()
        {
            var data = await db.user.Include(x => x.Role).Where(x => x.Role.RoleName.Contains("Employee")).CountAsync();
            return data;
        }

        public async Task<IEnumerable<EmployeeReportViewModel>> GetEmployeeReportsAsync()
        {
            return await db.user.Include(u => u.departments).Select(u => new EmployeeReportViewModel
            {
                UserId = u.UserId,
                Name = u.FirstName + " " + u.LastName,
                Email = u.Email,
                DepartmentName = u.departments.Name,
                PhoneNumber = u.PhoneNumber,
                DateOfJoining = u.DateOfJoining,
                Status = u.Status
            }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeReportViewModel>> SortEmployeeReportsAsync(string? sortType, string? sortStatus)
        {
            var query = db.user.Include(u => u.departments).AsQueryable();

            if (!string.IsNullOrEmpty(sortStatus))
            {
                query = query.Where(u => u.Status == sortStatus);
            }

            if (sortType == "Ascending")
            {
                query = query.OrderBy(u => u.DateOfJoining);
            }
            else if (sortType == "Descending")
            {
                query = query.OrderByDescending(u => u.DateOfJoining);

            }
            else if (sortType == "Last 7 days")
            {
                query = query.Where(u => u.DateOfJoining >= DateTime.Now.AddDays(-7));
            }
            else if (sortType == "Last Month")
            {
                query = query.Where(u => u.DateOfJoining >= DateTime.Now.AddMonths(-1));
            }
            else if (sortType == "Recently Added")
            {
                query = query.Where(u => u.DateOfJoining >= DateTime.Now.AddDays(-1));

            }

            var result = await query.Select(u => new EmployeeReportViewModel
            {
                UserId = u.UserId,
                Name = u.FirstName + " " + u.LastName,
                Email = u.Email,
                DepartmentName = u.departments.Name,
                PhoneNumber = u.PhoneNumber,
                DateOfJoining = u.DateOfJoining,
                Status = u.Status,
            }).ToListAsync();

            return result;
        }
    }
}
