using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login lg) { 
        

            if(!ModelState.IsValid)
            {
                return View(lg);
            }
            if (lg.Email == "Admin@gmail.com" && lg.Password == "admin123") {

                return RedirectToAction("Dashboard", "Admin");
            
            }

            var user = new Login
            {
                Email = lg.Email,
                Password = lg.Password
            };

            if (user == null) { 
            
                TempData["Error"] = "Invalid Email or Password";

            }
            return RedirectToAction("Dashboard", "User");

        }
    }
}