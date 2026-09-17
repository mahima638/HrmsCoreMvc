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

        public async Task<IActionResult> GetAllTaskMembers()
        {
            var taskMembers = await cs.GetAllTaskMembers();
            return View("~/Views/Project/GetAllTaskMembers.cshtml", taskMembers);
        }

        public async Task<IActionResult> AddTaskMember(TaskMembers taskMember)
        {
            if (ModelState.IsValid)
            {
                await cs.AddTaskMember(taskMember);
                TempData["SuccessMessage"] = "Task Member Added Successfully!";
                return RedirectToAction("GetAllTaskMembers");
            }
            return View(taskMember);
        }

        public async Task<IActionResult> UpdateTaskMember(TaskMembers taskMember)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateTaskMember(taskMember);
                TempData["SuccessMessage"] = "Task Member Updated Successfully!";
                return RedirectToAction("GetAllTaskMembers");
            }
            return View(taskMember);
        }

        public async Task<IActionResult> DeleteTaskMember(int taskMemberId)
        {
            await cs.DeleteTaskMember(taskMemberId);
            TempData["SuccessMessage"] = "Task Member Deleted Successfully!";
            return RedirectToAction("GetAllTaskMembers");
        }

        public async Task<IActionResult> SearchTaskMembers(string searchtaskmember)
        {
            var taskMembers = await cs.SearchTaskMembers(searchtaskmember);
            return View("GetAllTaskMembers", taskMembers);
        }
       
    }
}
