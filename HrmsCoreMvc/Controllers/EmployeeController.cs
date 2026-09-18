using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmpBankDetails Bd;
        private readonly IEmpEducation Ed;
        private readonly IEmpExperience Ex;

        private readonly IEmpFamilyInfo Fam;

        public EmployeeController(IEmpFamilyInfo Fam,IEmpExperience Ex, IEmpEducation Ed, IEmpBankDetails Bd)
        {
            this.Bd = Bd;
            this.Ed = Ed;
            this.Ex = Ex;
            this.Fam = Fam;
        }
        public IActionResult AddEmpBankDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpBankDetails(EmpBankDetails emp) {

            if (ModelState.IsValid) {

                await Bd.AddEmpBankDetails(emp);
                return RedirectToAction("GetEmpBankDetails");
            }
            return View();
        
        }
        public async Task<IActionResult> GetEmpBankDetails() {

           var emp = Bd.GetEmpBankDetails();
            return View(emp);
        }
        public async Task<IActionResult> DeleteEmpBankDetail(int id) {

           
           await Bd.DeleteEmpBankDetails(id);
            return RedirectToAction("GetEmpBankDetails");
        }

        public async Task<IActionResult> EditEmpBankDetails(int id)
        {

            var emp = Bd.GetEmpBankDetailsById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpBankDetails(EmpBankDetails emp)
        {

            if (ModelState.IsValid)
            {
                await Bd.EditEmpBankDetails(emp);
                return RedirectToAction("GetEmpBankDetails");
            }
            else
            {
                return View(emp);
            }


        }
        //public async Task<EmpBankDetails?> GetBankDetailsByUserId(int userId)
        //{
        //    return await db.EmpBankDetails
        //        .FirstOrDefaultAsync(x => x.UserId == userId);
        //}
        //education
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
                return RedirectToAction("GetEmpEducation");
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


            await Ed.DeleteEmpEducation(id);
            return RedirectToAction("GetEmpEducation");
        }

        public async Task<IActionResult> EditEmpEducation(int id)
        {

            var emp = Ed.GetEmpEducationById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpEducation(EmpEducation emp)
        {

            if (ModelState.IsValid)
            {
                await Ed.EditEmpEducation(emp);
                return RedirectToAction("GetEmpEducation");
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
                return RedirectToAction("GetEmpExperience");
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


            await Ex.DeleteEmpExperience(id);
            return RedirectToAction("GetEmpExperience");
        }

        public async Task<IActionResult> EditEmpExperience(int id)
        {

            var emp = Ex.GetEmpExperienceById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditExperience(EmpExperience emp)
        {

            if (ModelState.IsValid)
            { 
                await Ex.EditEmpExperience(emp);
                return RedirectToAction("GetEmpExperience");
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
        public async Task<IActionResult> AddEmpFamiltInfo(EmpFamilyInfo emp)
        {

            if (ModelState.IsValid)
            {

                await Fam.AddEmpFamilyInfo(emp);
                return RedirectToAction("GetEmpFamilyInfo");
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


            await Fam.DeleteEmpFamilyInfo(id);
            return RedirectToAction("GetEmpFamilyInfo");
        }

        public async Task<IActionResult> EditEmpFamilyInfo(int id)
        {

            var emp = Fam.GetEmpFamilyInfoById(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpFamilyInfo(EmpFamilyInfo emp)
        {

            if (ModelState.IsValid)
            {
                await Fam.EditEmpFamilyInfo(emp);
                return RedirectToAction("GetEmpFamilyInfo");
            }
            else
            {
                return View(emp);
            }
            //experience



        }
    }
}
