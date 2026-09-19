using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Reports
{
    public class TaskReportService : ITaskReportService
    {
        private readonly ApplicationDbContext db;

        public TaskReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<TaskChartDto> fetchCharts()
        {
            var tasks = await db.tasks.Where(x => x.Status != null).ToListAsync();

            if (tasks == null || !tasks.Any()) return new TaskChartDto();
            int completedTasks = tasks.Count(x => x.Status == "Completed");
            int pendingTasks = tasks.Count(x => x.Status == "Pending");
            int inprogressTasks = tasks.Count(x => x.Status == "Inprogress");
            int onholdTasks = tasks.Count(x => x.Status == "Onhold");

            double totaltasks = completedTasks + pendingTasks + inprogressTasks + onholdTasks;
            return new TaskChartDto
            {
                ChartLabels = new List<string> { "Completed", "Pending", "Inprogress", "OnHold" },
                ChartCompleted = new List<double> {  totaltasks > 0 ? Math.Round(((double)completedTasks/totaltasks)*100,2):0},
                ChartPending = new List<double> {  totaltasks > 0 ? Math.Round(((double)pendingTasks/totaltasks)*100,2):0},
                ChartInProgress = new List<double> {  totaltasks > 0 ? Math.Round(((double)inprogressTasks/totaltasks)*100,2):0},
                ChartOnHold = new List<double> { totaltasks > 0 ? Math.Round(((double)onholdTasks / totaltasks) * 100, 2) : 0 }

            };

        }

        public async Task<int> fetchCompletedTasks()
        {
            var data = await db.tasks.Where(t => t.Status == "Completed").CountAsync();
            return data;
        }

        public async Task<int> fetchOnHoldTasks()
        {
            var data = await db.tasks.Where(t => t.Status == "Onhold").CountAsync();
            return data;
        }

        public async Task<int> fetchOverdueTasks()
        {
            var data = await db.taskboards.Where(t => t.Duedate < DateTime.Now).CountAsync();
            return data;
        }

        public async Task<IEnumerable<TaskReportViewModel>> fetchTasks()
        {
            var data = await db.tasks.Include(t => t.Project).Include(t => t.TaskBoard).Select(t => new TaskReportViewModel()
            {
                TaskId = t.TaskId,
                TaskName = t.Title,
                ProjectName = t.Project.ProjectName,
                DueDate = t.TaskBoard.Duedate,
                Priority = t.Priority,
                Status = t.Status
            }).ToListAsync();

            return data;
        }

        public async Task<int> fetchTotalTasks()
        {
            var data = await db.tasks.Select(t => t.TaskId).CountAsync();
            return data;
        }

        public async Task<IEnumerable<TaskReportViewModel>> sortTasks(string? priority, string? status, string? sortType)
        {
            var query = db.tasks.Include(t => t.Project).Include(t => t.TaskBoard).AsQueryable();
            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(t => t.Priority == priority);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status == status);
            }
            if (sortType == "Ascending")
            {
                query = query.OrderBy(t => t.TaskBoard.Duedate);
            }
            else if (sortType == "Descending")
            {
                query = query.OrderByDescending(t => t.TaskBoard.Duedate);

            }
            else if (sortType == "Last Month")
            {
                query = query.Where(t => t.TaskBoard.Duedate >= DateTime.Now.AddMonths(-1));
            }
            else if(sortType == "Last 7 Days")
            {
                query = query.Where(t => t.TaskBoard.Duedate >= DateTime.Now.AddDays(-7));
            }
            else if (sortType == "Recently Added")
            {
                query = query.Where(t => t.TaskBoard.Duedate >= DateTime.Now.AddDays(-1));

            }

            var result = await query.Select(t => new TaskReportViewModel
            {

                TaskId = t.TaskId,
                TaskName = t.Title,
                ProjectName = t.Project.ProjectName,
                DueDate = t.TaskBoard.Duedate,
                Priority = t.Priority,
                Status = t.Status
            }).ToListAsync();
            return result;

        }
    }
}
