using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HrmsCoreMvc.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly ApplicationDbContext db;
        public DesignationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddDesignation(Designation designation)
        {
            db.Add(designation);
            await  db.SaveChangesAsync();
        }

        public async Task<List<Designation>> GetAllDesignations()
        {
            return await  db.designation.ToListAsync();
        }

        public async Task<Designation> getDesignationById(int id)
        {
            return await db.designation.FindAsync(id);
        }

        public async Task RemoveDesignation(int id)
        {
            var des = db.designation.Find(id);
            db.designation.Remove(des);
            await db.SaveChangesAsync();
        }

        public async Task UpdateDesignation(Designation designation)
        {
            db.Update(designation);
            await db.SaveChangesAsync();
        }
    }
}
