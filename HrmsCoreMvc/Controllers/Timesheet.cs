using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class Timesheet : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
