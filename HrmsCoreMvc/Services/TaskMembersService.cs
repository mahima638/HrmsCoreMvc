using HrmsCoreMvc.Models.Projects;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;
using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Services
{
    public class TaskMembersService : ITaskMembersService
    {
        private readonly ApplicationDbContext db;

        public TaskMembersService(ApplicationDbContext tms)
        {
            db = tms;
        }

        public async Task<List<TaskMembers>> GetAllTaskMembers()
        {
            return await db.taskmembers.ToListAsync();
        }

        public async Task<string> AddTaskMember(TaskMembers taskMember)
        {
            db.taskmembers.Add(taskMember);
            await db.SaveChangesAsync();
            return "Task member added successfully";
        }

        public async Task<string> UpdateTaskMember(TaskMembers taskMember)
        {
            db.taskmembers.Update(taskMember);
            await db.SaveChangesAsync();
            return "Task member updated successfully";
        }

        public async Task<string> DeleteTaskMember(int taskMemberId)
        {
            var taskMember = await db.taskmembers.FindAsync(taskMemberId);
            if (taskMember != null)
            {
                db.taskmembers.Remove(taskMember);
                await db.SaveChangesAsync();
                return "Task member deleted successfully";
            }
            return "Task member not found";
        }

        public async Task<List<TaskMembers>> SearchTaskMembers(string searchtaskmember)
        {
            return await db.taskmembers
                .Where(tm => tm.TaskMemberName != null && tm.TaskMemberName.Contains(searchtaskmember))
                .ToListAsync();
        }
    }
}
