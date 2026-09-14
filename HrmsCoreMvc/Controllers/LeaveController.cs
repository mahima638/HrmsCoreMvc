using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveService serive;
        public LeaveController(ILeaveService service)
        {
            this.serive = service;
        }
        public IActionResult Leave()
        {
            ViewBag.LeaveTypeList = serive.FetchLeaveTypeList();
            return View(new MasterLeaveType());
            serive.FetchDept();
            serive.FetchLeaveType();
            return View();
        }

        [HttpPost]
        public IActionResult Leave(MasterLeaveType m)
        {
            serive.AddLeaveType(m);
            return RedirectToAction("Leave");
        }
    }
}
