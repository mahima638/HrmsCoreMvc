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

        public List<AllProjects> GetAllProjects()
        {
            return db.AllProjects.ToList();
        }

        public string AddProject(AllProjects project)
        {
            db.AllProjects.Add(project);
            db.SaveChanges();
            return "Project added successfully";
        }

        public string UpdateProject(AllProjects project)
        {
            db.AllProjects.Update(project);
            db.SaveChanges();
            return "Project updated successfully";
        }

        public string DeleteProject(int projectId)
        {
            var project = db.AllProjects.Find(projectId);
            if (project != null)
            {
                db.AllProjects.Remove(project);
                db.SaveChanges();
                return "Project deleted successfully";
            }
            return "Project not found";
        }

        public List<AllProjects> SearchProjects(string searchproject)
        {
            return db.AllProjects
                .Where(p => p.ProjectName.Contains(searchproject) || p.ClientName.Contains(searchproject))
                .ToList();
        }
    }
}
