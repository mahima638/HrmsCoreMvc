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

        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await cs.GetAllTasks();
            return View("~/Views/Project/GetAllTasks.cshtml", tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                await cs.AddTask(task);
                TempData["SuccessMessage"] = "Task Added Successfully!";
                return RedirectToAction("GetAllTasks");
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(Task task)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateTask(task);
                TempData["SuccessMessage"] = "Task Updated Successfully!";
                return RedirectToAction("GetAllTasks");
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await cs.DeleteTask(taskId);
            TempData["SuccessMessage"] = "Task Deleted Successfully!";
            return RedirectToAction("GetAllTasks");
        }

        [HttpPost]
        public async Task<IActionResult> SearchTasks(string searchtask)
        {
            var tasks = await cs.SearchTasks(searchtask);
            return View("GetAllTasks", tasks);
        }
       
    }
}
