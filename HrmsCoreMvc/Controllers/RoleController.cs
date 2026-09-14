using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{


    public class RoleController : Controller
    {
        private readonly IRoleService rs;
        
        public RoleController(IRoleService rs)

        {
            this.rs = rs;
        }

        public IActionResult getRoles() {

            var roles = rs.GetAllRole();
            return View(roles);

        }
        public IActionResult RoleView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RoleView(Role role)
        {
           if(!ModelState.IsValid)
            {
                return View();
            }
            rs.AddRole(role);
            return RedirectToAction("getRoles");
        }
    }
}
