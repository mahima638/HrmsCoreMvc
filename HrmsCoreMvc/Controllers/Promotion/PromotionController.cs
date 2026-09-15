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

        // Promotion List
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionRepository.GetPromotionsAsync();

            return View(promotions);
        }

        // Open Add Promotion Page
        [HttpGet]
        public async Task<IActionResult> AddPromotion()
        {
            var users = await _promotionRepository.GetUsersAsync();

            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();

            return View();
        }

        // Save New Promotion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(Promotion promotions)
        {
            if (ModelState.IsValid)
            {
                await _promotionRepository.AddPromotionAsync(promotions);

                TempData["SuccessMessage"] =
                    "Promotion added successfully.";

                return RedirectToAction("Index");
            }

            // Reload users if validation fails
            var users = await _promotionRepository.GetUsersAsync();

            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();

            return View(promotions);
        }

        // Open Edit Page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var promotion =
                await _promotionRepository.GetPromotionByIdAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            var users = await _promotionRepository.GetUsersAsync();

            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();

            return View(promotion);
        }

        // Update Promotion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePromotion(Promotion promotions)
        {
            if (ModelState.IsValid)
            {
                await _promotionRepository.UpdatePromotionAsync(promotions);

                TempData["SuccessMessage"] =
                    "Promotion updated successfully.";

                return RedirectToAction("Index");
            }

            var users = await _promotionRepository.GetUsersAsync();

            ViewBag.Users = users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();

            return View("Edit", promotions);
        }

        // Delete Promotion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var promotion =
                await _promotionRepository.GetPromotionByIdAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            await _promotionRepository.DeletePromotionAsync(id);

            TempData["SuccessMessage"] =
                "Promotion deleted successfully.";

            return RedirectToAction("Index");
        }

        // Get Users as JSON
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _promotionRepository.GetUsersAsync();

            return Json(users);
        }
    }
}