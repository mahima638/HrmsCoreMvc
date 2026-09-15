using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface ITaskBoardService
    {
        public Task<List<TaskBoard>> GetAllTaskBoards();
        public Task<string> AddTaskBoard(TaskBoard taskBoard);
        public Task<string> UpdateTaskBoard(TaskBoard taskBoard);
        public Task<string> DeleteTaskBoard(int taskBoardId);
        public Task<List<TaskBoard>> SearchTaskBoards(string searchtaskboard);
    }
}
