using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;

namespace HrmsCoreMvc.Controllers
{
    public class TaskMembersController : Controller
    {
        private readonly ITaskMembersService cs;

        public TaskMembersController(ITaskMembersService tms)
        {
            cs = tms;
        }

        public IActionResult GetAllTaskMembers()
        {
            var taskMembers = cs.GetAllTaskMembers();
            return View(taskMembers);
        }

        public IActionResult AddTaskMember(TaskMembers taskMember)
        {
            if (ModelState.IsValid)
            {
                cs.AddTaskMember(taskMember);
                TempData["SuccessMessage"] = "Task Member Added Successfully!";
                return RedirectToAction("GetAllTaskMembers");
            }
            return View(taskMember);
        }

        public IActionResult UpdateTaskMember(TaskMembers taskMember)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateTaskMember(taskMember);
                TempData["SuccessMessage"] = "Task Member Updated Successfully!";
                return RedirectToAction("GetAllTaskMembers");
            }
            return View(taskMember);
        }

        public IActionResult DeleteTaskMember(int taskMemberId)
        {
            cs.DeleteTaskMember(taskMemberId);
            TempData["SuccessMessage"] = "Task Member Deleted Successfully!";
            return RedirectToAction("GetAllTaskMembers");
        }

        public IActionResult SearchTaskMembers(string searchtaskmember)
        {
            var taskMembers = cs.SearchTaskMembers(searchtaskmember);
            return View("GetAllTaskMembers", taskMembers);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
