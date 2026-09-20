using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmpBankDetails Bd;
        private readonly IEmpEducation Ed;
        private readonly IEmpExperience Ex;

        private readonly ApplicationDbContext db;
        private readonly IEmpFamilyInfo Fam;

        public EmployeeController(IEmpFamilyInfo Fam,IEmpExperience Ex, IEmpEducation Ed, IEmpBankDetails Bd, ApplicationDbContext db)
        {
            this.Bd = Bd;
            this.Ed = Ed;
            this.Ex = Ex;
            this.Fam = Fam;
            this.db = db;
        }
        public IActionResult AddEmpBankDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpBankDetails(EmpBankDetails emp) {

            if (ModelState.IsValid) {

                await Bd.AddEmpBankDetails(emp);
                return RedirectToAction("Details");
            }
            return View();
        
        }
        public async Task<IActionResult> GetEmpBankDetails() {

           var emp = Bd.GetEmpBankDetails();
            return View(emp);
        }
        public async Task<IActionResult> DeleteEmpBankDetail(int id) {

            var emp = await Bd.GetEmpBankDetailsById(id);
            await Bd.DeleteEmpBankDetails(id);
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> EditEmpBankDetails(int id)
        {

            var emp = await Bd.GetEmpBankDetailsById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpBankDetails(EmpBankDetails emp)
        {

            if (ModelState.IsValid)
            {
                await Bd.EditEmpBankDetails(emp);
                return RedirectToAction("Details");
            }
            else
            {
                return View(emp);
            }


        }
        public async Task<IActionResult> Details() {

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) {
                return RedirectToAction("Login", "Account");
            }
            var user = await db.user.FirstOrDefaultAsync(u => u.UserId == userId);
            var emp = new Employee
            {
                User = user,
                BankDetails = await Bd.GetBankDetailsByUserId(userId.Value),
                EducationDetails = await Ed.GetEducationByUserId(userId.Value),
                ExperienceDetails = await Ex.GetExperienceByUserId(userId.Value),
                FamilyDetails = await Fam.GetFamilyInfoByUserId(userId.Value)
            };
            return View(emp);

        }
        public IActionResult AddEmpEducation()
          {
            return View();
          }

        [HttpPost]
        public async Task<IActionResult> AddEmpEducation(EmpEducation emp)
        {

            if (ModelState.IsValid)
            {

                await Ed.AddEmpEducation(emp);
                return RedirectToAction("Details");
            }
            return View();

        }
        public async Task<IActionResult> GetEmpEducation()
        {

            var emp = Ed.GetEmpEducation();
            return View(emp);
        }
        public async Task<IActionResult> DeleteEmpEducation(int id)
        {
            var emp = await Bd.GetEmpBankDetailsById(id);

            await Ed.DeleteEmpEducation(id);
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> EditEmpEducation(int id)
        {

            var emp = await Ed.GetEmpEducationById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpEducation(EmpEducation emp)
        {

            if (ModelState.IsValid)
            {
                await Ed.EditEmpEducation(emp);
                return RedirectToAction("Details");
            }
            else
            {
                return View(emp);
            }
        }
            //education

            //experience
         public IActionResult AddEmpExperience()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpExperience(EmpExperience emp)
        {

            if (ModelState.IsValid)
            {

                await Ex.AddEmpExperience(emp);
                return RedirectToAction("Details");
            }
            return View();

        }
        public async Task<IActionResult> GetEmpExperience()
        {

            var emp = Ex.GetEmpExperience();
            return View(emp);
        }
        public async Task<IActionResult> DeleteEmpExperience(int id)
        {
            var emp = await Bd.GetEmpBankDetailsById(id);

            await Ex.DeleteEmpExperience(id);
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> EditEmpExperience(int id)
        {

            var emp = await  Ex.GetEmpExperienceById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpExperience(EmpExperience emp)
        {

            if (ModelState.IsValid)
            { 
                await Ex.EditEmpExperience(emp);
                return RedirectToAction("Details");
            }
            else
            {
                return View(emp);
            }
            //experience



        }


        //Family
        public IActionResult AddEmpFamilyInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpFamilyInfo(EmpFamilyInfo emp)
        {

            if (ModelState.IsValid)
            {

                await Fam.AddEmpFamilyInfo(emp);
                return RedirectToAction("Details");
            }
            return View();

        }
        public async Task<IActionResult> GetEmpFamilyInfo()
        {

            var emp = Fam.GetEmpFamilyInfo();
            return View(emp);
        }
        public async Task<IActionResult> DeleteEmpFamilyInfo(int id)
        {
            var emp = await Bd.GetEmpBankDetailsById(id);

            await Fam.DeleteEmpFamilyInfo(id);
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> EditEmpFamilyInfo(int id)
        {

            var emp = await  Fam.GetEmpFamilyInfoById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpFamilyInfo(EmpFamilyInfo emp)
        {

            if (ModelState.IsValid)
            {
                await Fam.EditEmpFamilyInfo(emp);
                return RedirectToAction("Details");
            }
            else
            {
                return View(emp);
            }
            //experience



        }
    }
}
