using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface IProjectService
    {
        public List<AllProjects> GetAllProjects();
        public string AddProject(AllProjects project);
        public string UpdateProject(AllProjects project);
        public string DeleteProject(int projectId);
        public List<AllProjects> SearchProjects(string searchproject);
    }
}
