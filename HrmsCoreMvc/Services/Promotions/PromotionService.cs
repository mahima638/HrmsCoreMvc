using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Repositories.Promotions;
using Microsoft.AspNetCore.Mvc;
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
            return _context.promotion.Include(p => p.User).FirstOrDefault(p => p.PId == promotionId);
        }

                public string AddPromotion(Promotion promotions)
        { 
            _context.promotion.Add(promotions);
            _context.SaveChanges();
            return "Added Successfully";
        }

        public string UpdatePromotion(Promotion promotions)
        {
            _context.promotion.Update(promotions);
            _context.SaveChanges();
            return "Data Updated Successfully";
        }

        public string DeletePromotion(int promotionId)
        {
            var promotion=_context.promotion.FirstOrDefault(p=>p.PId == promotionId);
            if( promotion != null)
            {
                _context.promotion.Remove(promotion);
                _context.SaveChanges();
            }
            return "Data Deleted Successfully";
        }

        public List<User> GetUsers()
        {
            return _context.user.ToList();
        }

    }
}
