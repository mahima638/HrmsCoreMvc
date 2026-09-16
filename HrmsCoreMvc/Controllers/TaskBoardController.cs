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

        public IActionResult GetAllTaskBoards()
        {
            var taskBoards = cs.GetAllTaskBoards();
            return View(taskBoards);
        }

        public IActionResult AddTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                cs.AddTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Added Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            return View(taskBoard);
        }

        public IActionResult UpdateTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Updated Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            return View(taskBoard);
        }

        public IActionResult DeleteTaskBoard(int taskBoardId)
        {
            cs.DeleteTaskBoard(taskBoardId);
            TempData["SuccessMessage"] = "Task Board Deleted Successfully!";
            return RedirectToAction("GetAllTaskBoards");
        }

        public IActionResult SearchTaskBoards(string searchtaskboard)
        {
            var taskBoards = cs.SearchTaskBoards(searchtaskboard);
            return View("GetAllTaskBoards", taskBoards);
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
