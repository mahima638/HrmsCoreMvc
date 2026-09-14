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

        public List<Task> GetAllTasks()
        {
            return db.tasks.ToList();
        }

        public string AddTask(Task task)
        {
            db.tasks.Add(task);
            db.SaveChanges();
            return "Task added successfully";
        }

        public string UpdateTask(Task task)
        {
            db.tasks.Update(task);
            db.SaveChanges();
            return "Task updated successfully";
        }

        public string DeleteTask(int taskId)
        {
            var task = db.tasks.Find(taskId);
            if (task != null)
            {
                db.tasks.Remove(task);
                db.SaveChanges();
                return "Task deleted successfully";
            }
            return "Task not found";
        }

        public List<Task> SearchTasks(string searchtask)
        {
            return db.tasks
                .Where(t => t.Title.Contains(searchtask) || t.Description.Contains(searchtask))
                .ToList();
        }
    }
}
