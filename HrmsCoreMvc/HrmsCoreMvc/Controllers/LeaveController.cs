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
        public async Task<IActionResult> Leave()
        {
            var LeaveTypeList = await serive.FetchLeaveTypeList();
            return View(LeaveTypeList);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(MasterLeaveType m)
        {
            await serive.AddLeaveType(m);
            return RedirectToAction("Leave");
        }

        public async Task<IActionResult> DeleteLeaveType(int leaveTypeId)
        {
            await  serive.DeleteLeaveType(leaveTypeId);
            return RedirectToAction("Leave");
        }

        [HttpGet]
        public async Task<IActionResult> AddLeaveDeptWise()
        {
            ViewBag.DepartmentList = await serive.FetchDept();
            ViewBag.LeaveTypeList = await serive.FetchLeaveType();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLeaveDeptWise(DepartmentLeaves model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            await serive.AllocateLeaveDeptwise(
                model.DepartmentId,
                model.LeaveTypeId,
                model.LeavesCount);

            TempData["Success"] = "Leave allocated successfully";

            return RedirectToAction("AddLeaveDeptWise");
        }

        public async Task<IActionResult> ShowDeptLeaveDetails()
        {
            var data = await serive.FetchDeptLeaveDetails();
            return View(data);
        }

        public async Task<IActionResult> LeaveSettings()
        {
            var leaveTypeList = await serive.FetchLeaveTypeList();
            return View(leaveTypeList);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLeaveTypeStatus(int leaveTypeId, bool isActive)
        {
            await serive.UpdateLeaveTypeStatus(leaveTypeId, isActive);

            return Json(new { success = true });
        }

    }
}
