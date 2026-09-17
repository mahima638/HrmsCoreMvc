using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface ITaskMembers
    {
        public List<TaskMembers> GetAllTaskMembers();
        public string AddTaskMember(TaskMembers taskMember);
        public string UpdateTaskMember(TaskMembers taskMember);
        public string DeleteTaskMember(int taskMemberId);
        public List<TaskMembers> SearchTaskMembers(string searchtaskmember);
    }
}
