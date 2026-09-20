using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Models.ViewModel;
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

        [HttpGet]
        public async Task<IActionResult> ApplyLeave()
        {
            ViewBag.LeaveTypeList = await serive.FetchLeaveType();

            var vm = new LeaveRequestViewModel
            {
                LeaveRequests = await serive.FetchLeaveRequests(2), // static UserId
                LeaveRequest = new LeaveRequest()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyLeave(LeaveRequest req)
        {
            //req.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            req.UserId = 2;
            Console.WriteLine($"StartDate = {req.StartDate}");
            Console.WriteLine($"EndDate = {req.EndDate}");
            await serive.ApplyLeave(req);
                TempData["Success"] = "Leave request submitted successfully";
                return RedirectToAction("ApplyLeave");
           
            
            ViewBag.LeaveTypeList = await serive.FetchLeaveType();

            return View(req);
        }

        [HttpGet]
        public async Task<IActionResult> LeaveRequests(LeaveRequestViewModel vm)
        {
            vm.LeaveRequest.UserId = 2;

           
                await serive.ApplyLeave(vm.LeaveRequest);
                TempData["Success"] = "Leave request submitted successfully";
                return RedirectToAction("ApplyLeave");
            

            ViewBag.LeaveTypeList = await serive.FetchLeaveType();
            vm.LeaveRequests = await serive.FetchLeaveRequests(2);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ManagerLeaveRequests()
        {
            var data = await serive.FetchManagerLeaveRequests(); // static ManagerId
            return View(data);
        }

        public async Task<IActionResult> ApproveLeave(int id)
        {
            //string managerName = HttpContext.Session.GetString("UserName");
            string managerName = "Krish"; 

            await serive.ApproveLeave(id, managerName);

            TempData["Success"] = "Leave approved successfully.";
            return RedirectToAction("ManagerLeaveRequests");
        }
        public async Task<IActionResult> RejectLeave(int id)
        {
            //string managerName = HttpContext.Session.GetString("UserName");
            string managerName = "Krish";

            await serive.RejectLeave(id, managerName);

            TempData["Success"] = "Leave rejected successfully.";
            return RedirectToAction("ManagerLeaveRequests");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterLeaveType model)
        {
           
                await serive.UpdateLeaveType(model);
                TempData["Success"] = "Leave Type Updated Successfully.";
            

            return RedirectToAction("Leave");
        }

        public async Task<IActionResult> DeleteDepartmentLeave(int id)
        {
            await serive.DeleteDepartmentLeave(id);

            TempData["Success"] = "Department leave deleted successfully.";
            return RedirectToAction("ShowDeptLeaveDetails");
        }
    }
}
