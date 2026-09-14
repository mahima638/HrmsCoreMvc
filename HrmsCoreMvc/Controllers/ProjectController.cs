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

        public ProjectController(IProjectService ps)
        {
            cs = ps;
        }

        public IActionResult GetAllProjects()
        {
            var projects = cs.GetAllProjects();
            return View(projects);
        }

        [HttpPost]
        public IActionResult AddProject(AllProjects project)
        {
            if (ModelState.IsValid)
            {
                cs.AddProject(project);
                TempData["SuccessMessage"] = "Project Added Successfully!";
                return RedirectToAction("GetAllProjects");
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult UpdateProject(AllProjects project)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateProject(project);
                TempData["SuccessMessage"] = "Project Updated Successfully!";
                return RedirectToAction("GetAllProjects");
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult DeleteProject(int projectId)
        {
            cs.DeleteProject(projectId);
            TempData["SuccessMessage"] = "Project Deleted Successfully!";
            return RedirectToAction("GetAllProjects");
        }

        [HttpPost]
        public IActionResult SearchProjects(string searchproject)
        {
            var projects = cs.SearchProjects(searchproject);
            return View("GetAllProjects", projects);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
