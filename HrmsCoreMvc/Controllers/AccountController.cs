using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace HrmsCoreMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db;
        public AccountController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
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
            //if (lg.RoleName == "Manager") {
            //    return RedirectToAction("Dashboard", "Manager");
            //}

           

           
            return RedirectToAction("Dashboard", "User");

        }
        //public IActionResult GoogleLogin()
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = "/Account/GoogleResponse"
        //    };

        //    return Challenge(
        //        properties,
        //        GoogleDefaults.AuthenticationScheme);
        //}

        //public IActionResult GoogleResponse()
        //{
        //    var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        //    if (email == "admin.hrms12@gmail.com")
        //    {
        //        return RedirectToAction("Dashboard", "Admin");
        //    }

        //    return RedirectToAction("Dashboard", "User");
        //}
    }
}