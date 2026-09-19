using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
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

        public async Task<ProjectChartDto> fetchCharts()
        {
            var projects = await db.AllProjects.Where(x => x.Status != null).Include(x => x.tasks).ToListAsync();

            if (projects == null || !projects.Any()) return new ProjectChartDto();

            int ActiveProjects = projects.Count(x => x.Status == "Active");
            int InActiveProjects = projects.Count(x => x.Status == "Inactive");
            int CompletedTasks = projects.Where(x => x.tasks != null).SelectMany(x => x.tasks).Count(t => t.Status == "Completed");
            int InprogressTasks = projects.Where(x => x.tasks != null).SelectMany(x => x.tasks).Count(t => t.Status == "Inprogress");

            double total = ActiveProjects + InActiveProjects + CompletedTasks + InprogressTasks;
            return new ProjectChartDto
            {
                Chartlabels = new List<string> { "Active Projects", "InActive Projects", "InProgress Tasks", "Completed Tasks" },
                ActiveProjects = new List<double> { total > 0 ? Math.Round(((double)ActiveProjects / total) * 100, 2): 0},
                InActiveProjects = new List<double> { total > 0 ? Math.Round(((double)InActiveProjects / total) * 100, 2) : 0 },
                InProgressTasks = new List<double> { total > 0 ? Math.Round(((double)InprogressTasks / total) * 100, 2) : 0 },
                CompletedTasks = new List<double> { total > 0 ? Math.Round(((double)CompletedTasks / total) * 100, 2) : 0 }
            };
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

        public async Task<IEnumerable<ProjectReportsViewModel>> fetchProjectReports()
        {
            var data = await db.AllProjects.Include(ap => ap.projectusers).Select(ap => new ProjectReportsViewModel
            {
                ProjectId = ap.ProjectId,
                ProjectName = ap.ProjectName,
                Leader = ap.ManagerName,
                Members = ap.projectusers.Where(pu => pu.user !=null).Select(pu => pu.user.ProfilePicture).ToList(),
                Deadline = ap.EndDate,
                Priority = ap.Priority,
                Status = ap.Status
            }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<ProjectReportsViewModel>> sortProjectReports(string? priorityType, string? statusType, string? sortType)
        {
            var query = db.AllProjects.Include(ap => ap.projectusers).AsQueryable();
            if (!string.IsNullOrEmpty(statusType))
            {
                query = query.Where(ap => ap.Status == statusType);
            }
            if (!string.IsNullOrEmpty(priorityType))
            {
                query = query.Where(ap => ap.Priority == priorityType);
            }
            if(sortType == "Ascending")
            {
                query = query.OrderBy(ap => ap.EndDate);
            }
            else if(sortType == "Descending")
            {
                query.OrderByDescending(ap => ap.EndDate);
            }
            var data = await query.Select(ap => new ProjectReportsViewModel
            {
                ProjectId = ap.ProjectId,
                ProjectName = ap.ProjectName,
                Leader = ap.ManagerName,
                Members = ap.projectusers.Where(pu => pu.user != null).Select(pu => pu.user.ProfilePicture).ToList(),
                Deadline = ap.EndDate,
                Priority = ap.Priority,
                Status = ap.Status
            }).ToListAsync();


            return data;
        }
    }
}
