using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EmpEducationService : IEmpEducation
    {
        private readonly ApplicationDbContext db;
        public EmpEducationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddEmpEducation(EmpEducation emped)
        {
            db.Add(emped);
            await db.SaveChangesAsync();

        }

        public async Task DeleteEmpEducation(int id)
        {
            var emp = await  db.EmpEducation.FindAsync(id);
            db.Remove(emp);
            await db.SaveChangesAsync();
        }

        public async Task EditEmpEducation(EmpEducation emped)
        {
            db.Update(emped);
            await db.SaveChangesAsync();
        }

        public async Task<List<EmpEducation>> GetEducationByUserId(int userId)
        {
            return await db.EmpEducation
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<EmpEducation>> GetEmpEducation()
        {
            return await db.EmpEducation.ToListAsync();
            
        }

        public async Task<EmpEducation> GetEmpEducationById(int id)
        {
           return await db.EmpEducation.FindAsync(id);

        }

        
    }
}
