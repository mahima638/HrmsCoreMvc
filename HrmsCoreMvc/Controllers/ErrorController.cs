using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
