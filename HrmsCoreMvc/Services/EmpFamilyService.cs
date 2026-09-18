using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EmpFamilyService : IEmpFamilyInfo
    {
        private readonly ApplicationDbContext db;
        public EmpFamilyService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddEmpFamilyInfo(EmpFamilyInfo empfam)
        {
            db.Add(empfam);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmpFamilyInfo(int id)
        {
            var emp = await db.EmpFamily.FindAsync(id);
            db.Remove(emp);
            await db.SaveChangesAsync();
        }

        public async Task EditEmpFamilyInfo(EmpFamilyInfo empfam)
        {
            db.Update(empfam);
            await db.SaveChangesAsync();
        }

        public async Task<List<EmpFamilyInfo>> GetEmpFamilyInfo()
        {
            return await db.EmpFamily.ToListAsync();
        }

        public async Task<EmpFamilyInfo> GetEmpFamilyInfoById(int id)
        {
            return await db.EmpFamily.FindAsync(id);
        }

        public Task<List<EmpFamilyInfo?>> GetFamilyInfoByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
