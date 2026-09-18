using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface ITaskMembersService
    {
        public Task<List<TaskMembers>> GetAllTaskMembers();
        public Task<string> AddTaskMember(TaskMembers taskMember);
        public Task<string> UpdateTaskMember(TaskMembers taskMember);
        public Task<string> DeleteTaskMember(int taskMemberId);
        public Task<List<TaskMembers>> SearchTaskMembers(string searchtaskmember);
    }
}
