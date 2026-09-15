using HrmsCoreMvc.Data;
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

        public List<Promotion> GetPromotions()
        {
            return _context.promotion.Include(p => p.User).ToList();
        }

        public Promotion GetPromotionById(int promotionId)
        {
            return _context.promotion.Include(p => p.User).FirstOrDefault( p => p.PId == promotionId);
        }


        public void Add(Promotion promotions)
        { 
            _context.promotion.Add(promotions);
            _context.SaveChanges();
        }
    }
}
