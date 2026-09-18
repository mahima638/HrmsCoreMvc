using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Repositories.Promotions;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Promotions
{
    public class PromotionService : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;
        public PromotionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            return await _context.promotion
                .Include(p => p.User)
                .ToListAsync();
        }
        public async Task<Promotion?> GetPromotionByIdAsync(int promotionId)
        {
            return await _context.promotion
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PId == promotionId);
        }
        public async Task<string> AddPromotionAsync(Promotion promotions)
        {
            await _context.promotion.AddAsync(promotions);
            await _context.SaveChangesAsync();
            return "Added Successfully";
        }
        public async Task<string> UpdatePromotionAsync(Promotion promotions)
        {
            _context.promotion.Update(promotions);
            await _context.SaveChangesAsync();
            return "Data Updated Successfully";
        }
        public async Task<string> DeletePromotionAsync(int promotionId)
        {
            var promotion = await _context.promotion
                .FirstOrDefaultAsync(p => p.PId == promotionId);
            if (promotion != null)
            {
                _context.promotion.Remove(promotion);
                await _context.SaveChangesAsync();
            }
            return "Data Deleted Successfully";
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.user.ToListAsync();
        }

        public async Task<List<Designation>> GetDesignationsAsync()
        {
            return await _context.designation.ToListAsync();
        }
    }
}