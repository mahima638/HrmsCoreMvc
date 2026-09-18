using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EmpExperienceService : IEmpExperience
    {
        private readonly ApplicationDbContext db;
        public EmpExperienceService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddEmpExperience(EmpExperience empex)
        {
            db.Add(empex);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmpExperience(int id)
        {
            var emp = await db.EmpExperience.FindAsync(id);
            db.Remove(emp);
            await db.SaveChangesAsync();
           
        }

        public async Task EditEmpExperience(EmpExperience empex)
        {
            db.Update(empex);
            await db.SaveChangesAsync();
        }

        public async Task<List<EmpExperience>> GetEmpExperience()
        {
            return await db.EmpExperience.ToListAsync();
        }

        public async Task<EmpExperience> GetEmpExperienceById(int id)
        {
            return await db.EmpExperience.FindAsync(id);
        }

        public Task<List<EmpExperience?>> GetExperienceByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
