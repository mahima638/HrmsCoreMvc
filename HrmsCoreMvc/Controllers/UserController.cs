using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using HrmsCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IDepartmentService deptService;

        private readonly IDesignationService desService;
        public UserController(IUserService userService, IRoleService roleService, IDepartmentService deptService, IDesignationService desService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.deptService = deptService;
            this.desService = desService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> AddEmployee()
        {
            var roles = await roleService.GetAllRole();
            var depts = await deptService.GetDepartments();
            var des = await desService.GetAllDesignations();
            ViewBag.Designation = des;
            ViewBag.Departments = depts;
            ViewBag.Role= roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(User us, IFormFile profilePicture)
        {
            if (ModelState.IsValid) {
                if (profilePicture != null)
                {
                    var fileName = profilePicture.FileName;

                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        profilePicture.CopyTo(stream);
                    }

                    us.ProfilePicture = "/uploads/" + fileName;
                }
                await userService.AddEmployee(us);
                return RedirectToAction("GetEmployee");
            }
            var roles =await  roleService.GetAllRole();
            var depts =await  deptService.GetDepartments();
            var des = await desService.GetAllDesignations();
            ViewBag.Designation = des;
            ViewBag.Departments = depts;
            ViewBag.Roles = roles;
            return View(us);
        }

        public async Task<IActionResult> DeleteEmployee(int id) {

            await userService.DeleteEmployee(id);
            return RedirectToAction("GetEmployee");
        }

        public async Task<IActionResult> GetEmployee() {

            var users = await userService.getEmployees();
            ViewBag.Role = await roleService.GetAllRole();
            ViewBag.Departments = await deptService.GetDepartments();
            ViewBag.Designation = await desService.GetAllDesignations();
            return View(users);
        }

        public async Task<IActionResult> EditEmployee(int id) {
            var us =await userService.GetEmpById(id);
            return View(us);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(User us) {
            if (ModelState.IsValid)
            {
                await userService.UpdateEmployee(us);
                return RedirectToAction("GetEmployee");
            }
            return View();
        }
    }
}
