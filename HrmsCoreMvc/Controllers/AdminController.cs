using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
