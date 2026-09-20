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
        private readonly ApplicationDbContext db;
        public TaskBoardController(ITaskBoardService tbs, ApplicationDbContext db)
        {
            cs = tbs;
            this.db = db;
        }

        public async Task<IActionResult> GetAllTaskBoards()
        {
            var taskBoards = await cs.GetAllTaskBoards();
            return View("GetAllTaskBoards", taskBoards);
        }

        [HttpGet]
        public IActionResult AddTaskBoard()
        {
            ViewBag.Projects = db.AllProjects.ToList();
            ViewBag.Tasks = db.tasks.ToList();
            return View("AddTaskBoard");
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                await cs.AddTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Added Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            ViewBag.Projects = db.AllProjects.ToList();
            ViewBag.Tasks = db.tasks.ToList();
            return View("AddTaskBoard", taskBoard);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTaskBoard(int taskBoardId)
        {
            var board = (await cs.GetAllTaskBoards()).FirstOrDefault(x => x.TaskBoardId == taskBoardId);
            return View("UpdateTaskBoard", board);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskBoard(TaskBoard taskBoard)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateTaskBoard(taskBoard);
                TempData["SuccessMessage"] = "Task Board Updated Successfully!";
                return RedirectToAction("GetAllTaskBoards");
            }
            return View("UpdateTaskBoard", taskBoard);
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
