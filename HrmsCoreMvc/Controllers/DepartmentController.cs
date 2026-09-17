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
        public async Task<IActionResult> getDepartment() {
        
            var dept = await ds.GetDepartments();
            return View(dept);


        }
        public IActionResult AddDepartment()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Departments dept) {

            if (!ModelState.IsValid) {
                return View();
            }
           
            await ds.AddDepartment(dept);
            return RedirectToAction("getDepartment");

        }


        public async Task<IActionResult> DeleteDepartment(int id) {
        
            await ds.DeleteDepartment(id);
            return RedirectToAction("getDepartment");

        }

        public async Task<IActionResult> EditDepartment(int id) {
        
            var dept = await ds.GetDepartmentById(id);
            return View(dept);

        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(Departments dept) {

            if (ModelState.IsValid) { 
            
              await ds.UpdateDepartment(dept);
              return RedirectToAction("getDepartment");
            }
            return View(dept);
        }
    }
}
