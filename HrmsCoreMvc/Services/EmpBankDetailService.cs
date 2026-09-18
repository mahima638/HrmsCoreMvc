using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    

    public class EmpBankDetailService : IEmpBankDetails
    {
        private readonly ApplicationDbContext db;
        public EmpBankDetailService(ApplicationDbContext db)
        {
            this.db = db;
            
        }
        public async Task AddEmpBankDetails(EmpBankDetails empbd)
        {
            db.Add(empbd);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmpBankDetails(int id)
        {
            var emp = await  db.EmpBankDetails.FindAsync(id);
            db.Remove(emp);
            await db.SaveChangesAsync();
        }

        public async Task EditEmpBankDetails(EmpBankDetails empbd)
        {
            db.Update(empbd);
            await db.SaveChangesAsync();
        }

        public Task<EmpBankDetails?> GetBankDetailsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmpBankDetails>> GetEmpBankDetails()
        {
            return await db.EmpBankDetails.ToListAsync();
        }

        public async Task<EmpBankDetails> GetEmpBankDetailsById(int id)
        {
            return await db.EmpBankDetails.FindAsync(id);
        }


    }
}


