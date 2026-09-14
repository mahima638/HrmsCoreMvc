using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

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

           

           
            return RedirectToAction("Dashboard", "User");

        }
    }
}