using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;
using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Controllers
{
    public class TaskController : Controller
    {
        public readonly ITaskService cs;
        public TaskController(ITaskService ts)
        {
            cs = ts;
        }

        public IActionResult GetAllTasks()
        {
            var tasks = cs.GetAllTasks();
            return View(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                cs.AddTask(task);
                TempData["SuccessMessage"] = "Task Added Successfully!";
                return RedirectToAction("GetAllTasks");
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult UpdateTask(Task task)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateTask(task);
                TempData["SuccessMessage"] = "Task Updated Successfully!";
                return RedirectToAction("GetAllTasks");
            }
            return View(task);
        }

        [HttpPost]
        public IActionResult DeleteTask(int taskId)
        {
            cs.DeleteTask(taskId);
            TempData["SuccessMessage"] = "Task Deleted Successfully!";
            return RedirectToAction("GetAllTasks");
        }

        [HttpPost]
        public IActionResult SearchTasks(string searchtask)
        {
            var tasks = cs.SearchTasks(searchtask);
            return View("GetAllTasks", tasks);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
