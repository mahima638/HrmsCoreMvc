using HrmsCoreMvc.Models.Projects;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;
using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext db;
        public TaskService(ApplicationDbContext cs)
        {
            db = cs;
        }

        public async Task<List<Task>> GetAllTasks()
        {
            return await db.tasks.Include(t => t.Project).ToListAsync();
        }

        public async Task<string> AddTask(Task task)
        {
            db.tasks.Add(task);
            await db.SaveChangesAsync();
            return "Task added successfully";
        }

        public async Task<string> UpdateTask(Task task)
        {
            db.tasks.Update(task);
            await db.SaveChangesAsync();
            return "Task updated successfully";
        }

        public async Task<string> DeleteTask(int taskId)
        {
            var task = await db.tasks.FindAsync(taskId);
            if (task != null)
            {
                db.tasks.Remove(task);
                await db.SaveChangesAsync();
                return "Task deleted successfully";
            }
            return "Task not found";
        }

        public async Task<List<Task>> SearchTasks(string searchtask)
        {
            return await db.tasks
                .Where(t => t.Title.Contains(searchtask) || t.Description.Contains(searchtask))
                .ToListAsync();
        }
    }
}
