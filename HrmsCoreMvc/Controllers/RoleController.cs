using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;


namespace HrmsCoreMvc.Controllers
{


    public class RoleController : Controller
    {

        private readonly IRoleService rs;
        

        public RoleController(IRoleService rs)


        {
            this.rs = rs;
        }

        public async Task<IActionResult> getRoles() {

            var roles = await  rs.GetAllRole();
            return View(roles);

        }
        public IActionResult RoleView()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RoleView(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await rs.AddRole(role);
            return RedirectToAction("getRoles");
        }


        public async Task<IActionResult> DeleteRole(int id) { 
        
           await rs.DeleteRole(id);

            return RedirectToAction("getRoles");

        }

        public async Task<IActionResult> EditRole(int id) { 
            var role = await rs.GetRoleById(id);
            return View(role);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Role role) {

            if (ModelState.IsValid)
            {
                await rs.EditRole(role);
                return RedirectToAction("getRoles");

            }
            else { 
              return View(role);
            }
        
        }


    }
}
