using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EarningService : IEarningService
    {
        private readonly ApplicationDbContext db;
        public EarningService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddEarningType(EarningType e)
        {
            db.EarningType.AddAsync(e);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEarning(int id)
        {
            var earningType = await db.Earning.FindAsync(id);
            if (earningType != null)
            {
                db.Earning.Remove(earningType);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteEarningType(int id)
        {
            var earningType = await db.EarningType.FindAsync(id);
            if (earningType != null)
            {
                db.EarningType.Remove(earningType);
                await db.SaveChangesAsync();
            }
        }

       
        public async Task AddEarning(Earning earning)
        {
            db.Earning.AddAsync(earning);
            await db.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> FetchDepartment()
        {
            return await db.department
                .Where(d => d.Status == "Active")
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> FetchDesignation()
        {
            return await db.designation
                .Where(d => d.status == "Active")
                .Select(d => new SelectListItem
                {
                    Value = d.DesignationId.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }

        public async Task<Earning?> FetchEarningById(int id)
        {
            return await db.Earning.FindAsync(id);
        }



        public async Task<List<Earning>> FetchEarningList()
        {
            return await db.Earning
                .Include(e => e.EarningType)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .ToListAsync();

        }

        public async Task<List<SelectListItem>> FetchEarningType()
        {
            return await db.EarningType
                .Select(e => new SelectListItem
                {
                    Value = e.EarntypeId.ToString(),
                    Text = e.EarningName
                })
                .ToListAsync();
        }

        public Task UpdateEarning(Earning earning)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EarningType>> FetchEarningTypeList()
        {
            return await db.EarningType.ToListAsync();
        }

        public async Task<EarningType?> FetchEarningTypeById(int id)
        {
            return await db.EarningType.FindAsync(id);
        }

        public async Task UpdateEarningType(EarningType e)
        {
            var earningType = await db.EarningType.FindAsync(e.EarntypeId);

            if (earningType != null)
            {
                earningType.EarningName = e.EarningName;

                await db.SaveChangesAsync();
            }

        }

    }
}
