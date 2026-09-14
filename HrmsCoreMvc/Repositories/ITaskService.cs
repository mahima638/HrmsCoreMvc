using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Repositories
{
    public interface ITaskService
    {
        public List<Task> GetAllTasks();
        public string AddTask(Task task);
        public string UpdateTask(Task task);
        public string DeleteTask(int taskId);
        public List<Task> SearchTasks(string searchtask);
    }
}
