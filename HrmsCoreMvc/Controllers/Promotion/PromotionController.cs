using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Repositories.Promotions;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository _promotionRepository;
        public PromotionController(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var promotions =await _promotionRepository.GetPromotionsAsync();
            return View(promotions);
        }

        [HttpGet]
        public async Task<IActionResult> AddPromotion()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(Promotion promotions)
        {
            if (ModelState.IsValid)
            {
                await _promotionRepository.AddPromotionAsync(promotions);
                TempData["SuccessMessage"] ="Promotion added successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View(promotions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var promotion =await _promotionRepository.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            await LoadDropdowns();
            return View(promotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePromotion(Promotion promotions)
        {
            if (ModelState.IsValid)
            {
                await _promotionRepository.UpdatePromotionAsync(promotions);

                TempData["SuccessMessage"] ="Promotion updated successfully.";
                return RedirectToAction("Index");
            }
            await LoadDropdowns();
            return View("Edit", promotions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await _promotionRepository.DeletePromotionAsync(id);
            TempData["SuccessMessage"] ="Promotion deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users =await _promotionRepository.GetUsersAsync();
            return Json(users);
        }
        private async Task LoadDropdowns()
        {
            var users =await _promotionRepository.GetUsersAsync();
            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            var designations =await _promotionRepository.GetDesignationsAsync();
            ViewBag.Designations = designations;
        }
    }
}