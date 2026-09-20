using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService desService;
        private readonly IDepartmentService deptService;
        public DesignationController(IDesignationService desService, IDepartmentService deptService)
        {
            this.desService= desService;
            this.deptService= deptService;
            
        }

        public async Task<IActionResult> AddDesignation() {
            var departments = await deptService.GetDepartments();
            ViewBag.Departments = departments;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddDesignation(Designation des) {

          
            if (!ModelState.IsValid) {
                var departments = await deptService.GetDepartments();
                ViewBag.Departments = departments;
                return View(des);
            
            }
            await desService.AddDesignation(des);
            
            return RedirectToAction("GetDesignation");
           
        
        }
       
        public async Task<IActionResult> DeleteDesignation(int id) {
            await desService.RemoveDesignation(id);
            return RedirectToAction("GetDesignation");
        
        }
        public async Task<IActionResult> GetDesignation() {
            var des = await desService.GetAllDesignations();
            var departments = await deptService.GetDepartments();
            ViewBag.Departments = departments;
            return View(des);
        
        }

        public async Task<IActionResult> EditDesignation(int id) { 
        
        var des = await desService.getDesignationById(id);
          
            return View(des);
          
        }
        [HttpPost]
        public async Task<IActionResult> EditDesignation(Designation des)
        {
            if (!ModelState.IsValid) {
                return View();
            }
            await desService.UpdateDesignation(des);
            return RedirectToAction("GetDesignation");
        }
        

    }
}
