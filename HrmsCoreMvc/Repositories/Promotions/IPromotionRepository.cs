using HrmsCoreMvc.Models.Promotion;

namespace HrmsCoreMvc.Repositories.Promotions
{
    public interface IPromotionRepository
    {
        List<Promotion> GetPromotions();
        Promotion GetPromotionById(int promotionId);
    }
}
