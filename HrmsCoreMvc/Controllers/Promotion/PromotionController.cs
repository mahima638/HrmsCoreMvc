using HrmsCoreMvc.Repositories.Promotions;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Promotion
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


        public IActionResult Edit(int id)
        {
            var promotion = _promotionRepository.GetPromotionById(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }
    }
}
