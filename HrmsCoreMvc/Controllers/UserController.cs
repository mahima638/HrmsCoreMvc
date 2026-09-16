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
        public IActionResult AddEmployee()
        {
            var roles = roleService.GetAllRole();
            var depts = deptService.GetDepartments();
            var des = desService.GetAllDesignations();
            ViewBag.Designation = des;
            ViewBag.Departments = depts;
            ViewBag.Role= roles;
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(User us, IFormFile profilePicture)
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
                userService.AddEmployee(us);
                return RedirectToAction("GetEmployee");
            }
            var roles = roleService.GetAllRole();
            var depts = deptService.GetDepartments();
            var des = desService.GetAllDesignations();
            ViewBag.Designation = des;
            ViewBag.Departments = depts;
            ViewBag.Roles = roles;
            return View(us);
        }

        public IActionResult DeleteEmployee(int id) {

            userService.DeleteEmployee(id);
            return RedirectToAction("GetEmployee");
        }

        public IActionResult GetEmployee() {

            var users = userService.getEmployees();
            ViewBag.Role = roleService.GetAllRole();
            ViewBag.Departments = deptService.GetDepartments();
            ViewBag.Designation = desService.GetAllDesignations();
            return View(users);
        }

        public IActionResult EditEmployee(int id) {
            var us =userService.GetEmpById(id);
            return View(us);
        }
        [HttpPost]
        public IActionResult EditEmployee(User us) {
            if (ModelState.IsValid)
            {
                userService.UpdateEmployee(us);
                return RedirectToAction("GetEmployee");
            }
            return View();
        }
    }
}
