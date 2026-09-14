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
            var data = await db.tasks.Include(t => t.projects).Select(t => new TaskReportViewModel()
            {
                TaskId = t.TaskId,
                TaskName = t.Title,
                ProjectName = t.projects.ProjectName,
                DueDate = t.
            });
        }

        public async Task<int> fetchTotalTasks()
        {
            var data = await db.tasks.Select(t => t.TaskId).CountAsync();
            return data;
        }


    }
}
