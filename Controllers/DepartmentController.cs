using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class DepartmentController : Controller
    {
       
        private readonly IDepartmentService ds;

        public DepartmentController(IDepartmentService ds)
        {
            this.ds = ds;
        }
        public IActionResult getDepartment() {
        
            var dept = ds.GetDepartments();
            return View(dept);


        }
        public IActionResult AddDepartment()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddDepartment(Departments dept) {

            if (!ModelState.IsValid) {
                return View();
            }
           
            ds.AddDepartment(dept);
            return RedirectToAction("getDepartment");

        }


        public IActionResult DeleteDepartment(int id) {
        
            ds.DeleteDepartment(id);
            return RedirectToAction("getDepartment");

        }

        public IActionResult EditDepartment(int id) {
        
            var dept = ds.GetDepartmentById(id);
            return View(dept);

        }

        [HttpPost]
        public IActionResult EditDepartment(Departments dept) {

            if (ModelState.IsValid) { 
            
              ds.UpdateDepartment(dept);
              return RedirectToAction("getDepartment");
            }
            return View(dept);
        }
    }
}
