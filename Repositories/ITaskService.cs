using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Repositories
{
    public interface ITaskService
    {
             

        public Task<List<Task>> GetAllTasks();
        public Task<string> AddTask(Task task);
        public Task<string> UpdateTask(Task task);
        public Task<string> DeleteTask(int taskId);
        public Task<List<Task>> SearchTasks(string searchtask);

    }
}
