using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
namespace HrmsCoreMvc.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddEmployee(User us)
        {
            db.Add(us);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            db.Remove(id);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetEmpById(int id)
        {
            return await  db.user.FindAsync(id);
           
        }

        public async Task<List<User>> getEmployees()
        {
            return await db.user.ToListAsync();
        }

        public async Task UpdateEmployee(User us)
        {
            db.Update(us);
            await db.SaveChangesAsync();
        }
    }
}
