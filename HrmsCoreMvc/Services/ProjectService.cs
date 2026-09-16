using HrmsCoreMvc.Models.Projects;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Data;
using HrmsCoreMvc.Repositories;

namespace HrmsCoreMvc.Services

{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext db;

        public ProjectService(ApplicationDbContext cs)
        {
             db = cs;
        }

        public async Task<List<AllProjects>> GetAllProjects()
        {
            return await db.AllProjects.ToListAsync();
        }

        public async Task<string> AddProject(AllProjects project)
        {
            db.AllProjects.Add(project);
            await db.SaveChangesAsync();
            return "Project added successfully";
        }

        public async Task<string> UpdateProject(AllProjects project)
        {
            db.AllProjects.Update(project);
            await db.SaveChangesAsync();
            return "Project updated successfully";
        }

        public async Task<string> DeleteProject(int projectId)
        {
            var project = await db.AllProjects.FindAsync(projectId);
            if (project != null)
            {
                db.AllProjects.Remove(project);
                await db.SaveChangesAsync();
                return "Project deleted successfully";
            }
            return "Project not found";
        }

        public async Task<List<AllProjects>> SearchProjects(string searchproject)
        {
            return await db.AllProjects
                .Where(p => p.ProjectName.Contains(searchproject) || p.ClientName.Contains(searchproject))
                .ToListAsync();
        }
    }
}
