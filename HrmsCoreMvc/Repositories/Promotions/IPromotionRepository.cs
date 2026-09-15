using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Promotion;

namespace HrmsCoreMvc.Repositories.Promotions
{
    public interface IPromotionRepository
    {
        List<Promotion> GetPromotions();
        Promotion GetPromotionById(int promotionId);
        string AddPromotion(Promotion promotions);
        string UpdatePromotion(Promotion promotions);
        string DeletePromotion(int promotionId);
        List<User> GetUsers();
    }
}
