using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HrmsCoreMvc.Controllers
{


    public class RoleController : Controller
    {
        private readonly RoleService rs;
        
        public RoleController(RoleService rs)

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

        public IActionResult DeleteRole(int id) { 
        
           rs.DeleteRole(id);

            return RedirectToAction("getRoles");

        }

        public IActionResult EditRole(int id) { 
            var role = rs.GetRoleById(id);
            return View(role);

        }

        [HttpPost]
        public IActionResult EditRole(Role role) {

            if (ModelState.IsValid)
            {
                rs.EditRole(role);
                return RedirectToAction("getRoles");

            }
            else { 
              return View(role);
            }
        
        }

    }
}
