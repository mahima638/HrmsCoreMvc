using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.ViewModel;

namespace HrmsCoreMvc.Controllers
{
    public class AttendanceController : Controller
    {
        private IAttendanceService service;
        public AttendanceController(IAttendanceService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

            AttendanceViewModel dashboard = await service.GetDashboard(userId);

            return View(dashboard);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

            await service.CheckIn(userId);

            TempData["Success"] = "Check In Successful";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LunchIn()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

            await service.LunchIn(userId);

            TempData["Success"] = "Lunch In Successful";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LunchOut()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

            await service.LunchOut(userId);

            TempData["Success"] = "Lunch Out Successful";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int userId = 2;

            await service.CheckOut(userId);

            TempData["Success"] = "Check Out Successful";

            return RedirectToAction("Index");
        }
    }
}
