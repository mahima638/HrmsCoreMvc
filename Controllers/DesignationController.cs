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

        public IActionResult AddDesignation() {
            var departments = deptService.GetDepartments();
            ViewBag.Departments = departments;
            return View();

        }
        [HttpPost]
        public IActionResult AddDesignation(Designation des) {

          
            if (!ModelState.IsValid) {
                var departments = deptService.GetDepartments();
                ViewBag.Departments = departments;
                return View(des);
            
            }
            desService.AddDesignation(des);
            
            return RedirectToAction("GetDesignation");
           
        
        }
       
        public IActionResult DeleteDesignation(int id) {
            desService.RemoveDesignation(id);
            return RedirectToAction("GetDesignation");
        
        }
        public IActionResult GetDesignation() {
            var des = desService.GetAllDesignations();
            return View(des);
        
        }

        public IActionResult EditDesignation(int id) { 
        
        var des = desService.getDesignationById(id);
            return View(des);
          
        }
        [HttpPost]
        public IActionResult EditDesignation(Designation des)
        {
            if (!ModelState.IsValid) {
                return View();
            }
            desService.UpdateDesignation(des);
            return RedirectToAction("GetDesignation");
        }
        

    }
}
