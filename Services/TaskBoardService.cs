using HrmsCoreMvc.Models.Projects;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;
using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly ApplicationDbContext db;
        public TaskBoardService(ApplicationDbContext cs)
        {
            db = cs;
        }
        public async Task<List<TaskBoard>> GetAllTaskBoards()
        {
            return await db.taskboards.ToListAsync();
        }

        public async Task<string> AddTaskBoard(TaskBoard taskBoard)
        {
            db.taskboards.Add(taskBoard);
            await db.SaveChangesAsync();
            return "Task board added successfully";
        }

        public async Task<string> UpdateTaskBoard(TaskBoard taskBoard)
        {
            db.taskboards.Update(taskBoard);
            await db.SaveChangesAsync();
            return "Task board updated successfully";
        }

        public async Task<string> DeleteTaskBoard(int taskBoardId)
        {
            var taskBoard = await db.taskboards.FindAsync(taskBoardId);
            if (taskBoard != null)
            {
                db.taskboards.Remove(taskBoard);
                await db.SaveChangesAsync();
                return "Task board deleted successfully";
            }
            return "Task board not found";
        }

        public async Task<List<TaskBoard>> SearchTaskBoards(string searchtaskboard)
        {
            return await db.taskboards
                .Where(t => t.TaskBoardName.Contains(searchtaskboard))
                .ToListAsync();
        }
    }
}
