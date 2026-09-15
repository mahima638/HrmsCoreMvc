using HrmsCoreMvc.Models.Projects;
namespace HrmsCoreMvc.Repositories
{
    public interface IProjectService
    {
        public Task<List<AllProjects>> GetAllProjects();
        public Task<string> AddProject(AllProjects project);
        public Task<string> UpdateProject(AllProjects project);
        public Task<string> DeleteProject(int projectId);
        public Task<List<AllProjects>> SearchProjects(string searchproject);
    }
}
