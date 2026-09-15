using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface ITaskBoardService
    {
        public List<TaskBoard> GetAllTaskBoards();
        public string AddTaskBoard(TaskBoard taskBoard);
        public string UpdateTaskBoard(TaskBoard taskBoard);
        public string DeleteTaskBoard(int taskBoardId);
        public List<TaskBoard> SearchTaskBoards(string searchtaskboard);
    }
}
