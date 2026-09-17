using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Promotion;

namespace HrmsCoreMvc.Repositories.Promotions
{
    public interface IPromotionRepository
    {
            Task<List<Promotion>> GetPromotionsAsync();
            Task<Promotion?> GetPromotionByIdAsync(int promotionId);
            Task<string> AddPromotionAsync(Promotion promotions);
            Task<string> UpdatePromotionAsync(Promotion promotions);
            Task<string> DeletePromotionAsync(int promotionId);
            Task<List<User>> GetUsersAsync();
            Task<List<Designation>> GetDesignationsAsync();

    }
}
