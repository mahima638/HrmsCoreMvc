using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Termination;
using HrmsCoreMvc.Repositories.Promotions;
using HrmsCoreMvc.Repositories.Resignations;
using HrmsCoreMvc.Repositories.Terminations;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Terminations
{
    public class TerminationController : Controller
    {
        private readonly ITerminationRepository _terminationRepository;
        public TerminationController(ITerminationRepository terminationRepository)
        {
            _terminationRepository = terminationRepository;
        }
        public async Task<IActionResult> Index()
        {
            var terminations = await _terminationRepository.GetTerminationsAsync();
            return View(terminations);
        }


        [HttpGet]
        public async Task<IActionResult> AddTermination()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTermination(Termination terminations)
        {
            if(ModelState.IsValid)
            {
                await _terminationRepository.AddTerminationAsync(terminations);
                TempData["SuccessMessage"] = "Termination added successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View(terminations);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var termination = await _terminationRepository.GetTerminationByIdAsync(id);
            if (termination == null)
            {
                return NotFound();
            }
            await LoadDropdowns();
            return View(termination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTermination(Termination terminations)
        {
            if(ModelState.IsValid)
            {
                await _terminationRepository.UpdateTerminationAsync(terminations);
                TempData["SuccessMessage"] = "Termination updated successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View(terminations);
        }

        public async Task<IActionResult> DeleteTermination(int id)
        {

            await _terminationRepository.DeleteTerminationAsync(id);
            TempData["SuccessMessage"] = "Termination deleted successfully.";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _terminationRepository.GetUsersAsync();
            return Json(users);
        }

        private async Task LoadDropdowns()
        {
            var users = await _terminationRepository.GetUsersAsync();
            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            //var department = await _terminationRepository.GetDepartmentAsync();
            //ViewBag.Department = department;
        }
    }
}
