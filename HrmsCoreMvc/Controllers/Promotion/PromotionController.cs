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

        // my index page...
        public IActionResult Index()
        {
            var promotions = _promotionRepository.GetPromotions();
            return View(promotions);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var promotion = _promotionRepository.GetPromotionById(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        [HttpPost]
        public IActionResult Create(Promotion promotions)
        {

            if(ModelState.IsValid)
            {
                _promotionRepository.AddPromotion(promotions);
                TempData["SuccessMessage"] = "Promotion added successfully.";
                return RedirectToAction("Index");
            }
            return View(promotions);
        }

        [HttpPost]
        public IActionResult Update(Promotion promotions)
        {
            if (ModelState.IsValid)
            {
                _promotionRepository.UpdatePromotion(promotions);
                TempData["SuccessMessage"] = "Promotion Updated Successfully.";
                return RedirectToAction("Index");
            }
            return View(promotions);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var promotion=_promotionRepository.GetPromotionById(id);
            if(promotion == null)
            {
                return NotFound();
            }
            
            _promotionRepository.DeletePromotion(id);
            return RedirectToAction("Index");

        }
        public IActionResult GetUser()
        {
            var users=_promotionRepository.GetUsers();
            return Json(users);
        }

    }
}
