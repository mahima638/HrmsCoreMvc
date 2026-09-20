using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;


namespace HrmsCoreMvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService cs;    
        private readonly ApplicationDbContext db;
        public ProjectController(IProjectService ps, ApplicationDbContext db)
        {
            cs = ps;
            this.db = db;
        }

        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await cs.GetAllProjects();
            return View(projects);
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            ViewBag.TeamMembers = db.user.Select(u => new{
            u.UserId, FullName = u.FirstName + " " + u.LastName}).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(AllProjects project)
        {
            if (ModelState.IsValid)
            {
                await cs.AddProject(project);
                TempData["SuccessMessage"] = "Project Added Successfully!";
                return RedirectToAction("GetAllProjects");
            }

            ViewBag.TeamMembers = db.user.Select(u => new {
                u.UserId, FullName = u.FirstName + " " + u.LastName}).ToList();
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProject(int projectId)
        {
            var project = (await cs.GetAllProjects()).FirstOrDefault(p => p.ProjectId == projectId);
            return View("UpdateProject", project);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(AllProjects project)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateProject(project);
                TempData["SuccessMessage"] = "Project Updated Successfully!";
                return RedirectToAction("GetAllProjects");
            }
            return View("UpdateProject", project);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            await cs.DeleteProject(projectId);
            TempData["SuccessMessage"] = "Project Deleted Successfully!";
            return RedirectToAction("GetAllProjects");
        }

        [HttpPost]
        public async Task<IActionResult> SearchProjects(string searchproject)
        {
            var projects = await cs.SearchProjects(searchproject);
            return View("GetAllProjects", projects);
        }

    }
}
