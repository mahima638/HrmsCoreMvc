using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;

namespace HrmsCoreMvc.Controllers
{
    public class TaskBoardController : Controller
    {
        private readonly ITaskBoardService cs;
        public TaskBoardController(ITaskBoardService tbs)
        {
            cs = tbs;
        }

        public async Task<IActionResult> GetAllTaskBoards()
        {
            var taskBoards = await cs.GetAllTaskBoards();
            return View("~/Views/Project/GetAllTaskBoards.cshtml", taskBoards);
        }

        public async Task<IActionResult> AddTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                await cs.AddTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Added Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            return View(taskBoard);
        }

        public async Task<IActionResult> UpdateTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Updated Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            return View(taskBoard);
        }

        public async Task<IActionResult> DeleteTaskBoard(int taskBoardId)
        {
            await cs.DeleteTaskBoard(taskBoardId);
            TempData["SuccessMessage"] = "Task Board Deleted Successfully!";
            return RedirectToAction("GetAllTaskBoards");
        }

        public async Task<IActionResult> SearchTaskBoards(string searchtaskboard)
        {
            var taskBoards = await cs.SearchTaskBoards(searchtaskboard);
            return View("GetAllTaskBoards", taskBoards);
        }
        
    }
}
