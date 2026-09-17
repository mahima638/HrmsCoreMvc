using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Repositories.Promotions;
using HrmsCoreMvc.Repositories.Resignations;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class ResignationController : Controller
    {
        private readonly IResignationRepository _resignationRepository;
        public ResignationController(IResignationRepository resignationRepository)
        {
            _resignationRepository = resignationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var resignation =await _resignationRepository.GetResignationsAsync();
            return View(resignation);
        }

        [HttpGet]
        public async Task<IActionResult> AddResignation()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResignation(Resignation resignation)
        {
            if(ModelState.IsValid)
            {
                await _resignationRepository.AddResignationAsync(resignation);
                TempData["SuccessMessage"] = "Resignation added successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View(resignation);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var resignation = await _resignationRepository.GetResignationByIdAsync(id);
            if(resignation==null)
            {
                return NotFound();
            }
            await LoadDropdowns();
            return View(resignation);

        }

        [HttpPost]
        
        public async Task<IActionResult> UpdateResignation(Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                await _resignationRepository.UpdateResignationAsync(resignation);

                TempData["SuccessMessage"] = "Resignation updated successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View("Edit", resignation);
        }

        public async Task<IActionResult> DeleteResignation(int id)
        {
            await _resignationRepository.DeleteResignationAsync(id);
            TempData["SuccessMessage"] = "Resignation deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _resignationRepository.GetUsersAsync();
            return Json(users);
        }

        private async Task LoadDropdowns()
        {
            var users = await _resignationRepository.GetUsersAsync();
            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            var department = await _resignationRepository.GetDepartmentAsync();
            ViewBag.Department = department;
        }


    }
}
