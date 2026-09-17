using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
