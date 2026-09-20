using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService service;
        public TimesheetController(ITimesheetService service)
        {
            this.service = service;
        }

  
        public async Task<IActionResult> Index()
        {
            var timesheets = await service.GetTimesheets();

            ViewBag.ProjectList = (await service.GetProjects())
                .Select(p => new SelectListItem
                {
                    Value = p.ProjectId.ToString(),
                    Text = p.ProjectName
                }).ToList();

            return View("Timesheet", timesheets);
        }


        [HttpPost]
        public async Task<IActionResult> AddTimesheet(Timesheet t)
        {

            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

                 t.UserId = userId;
                 //t.CreatedBy = HttpContext.Session.GetString("Email");
                t.CreatedBy="ram@gmail.com";
                t.CreatedAt = DateTime.Now;
                t.Status = "Pending";

                await service.AddTimesheet(t);

                TempData["Success"] = "Timesheet added successfully.";
                return RedirectToAction("Index");
            

            
            ViewBag.ProjectList = (await service.GetProjects())
                .Select(p => new SelectListItem
                {
                    Value = p.ProjectId.ToString(),
                    Text = p.ProjectName
                }).ToList();

            return View("Timesheet", await service.GetTimesheets());
        }

        [HttpPost]
        public async Task<IActionResult> SendForApproval(List<int> ids)
        {
            await service.SendForApproval(ids);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AdminTimesheet()
        {
            var timesheets = await service.GetTimesheets();
            return View(timesheets);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveSelected(List<int> ids)
        {
            await service.ApproveSelected(ids);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RejectSelected(List<int> ids)
        {
            await service.RejectSelected(ids);
            return Ok();
        }
    }
}
