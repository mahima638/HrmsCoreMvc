using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HrmsCoreMvc.Services.Reports
{
    public class AllProjectsReportService : IAllProjectsService
    {
        private readonly ApplicationDbContext db;
        public AllProjectsReportService(ApplicationDbContext db)
        {
            this.db = db;
            
        }

        public async Task<int> fetchAllProjects()
        {
            var data = await db.AllProjects.Select(ap => ap.ProjectId).CountAsync();
            return data;
        }

        public async Task<int> fetchOnHoldProjects()
        {
            var data = await db.AllProjects.Where(ap => ap.Status == "Inactive").CountAsync();
            return data;
        }

        public async Task<int> fetchOverdueProjects()
        {
            var data = await db.AllProjects.Where(ap => ap.EndDate < DateTime.Now).CountAsync();
            return data;
        }


    }
}
