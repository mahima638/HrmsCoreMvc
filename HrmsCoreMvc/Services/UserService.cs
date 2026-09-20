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

        public async Task UpdateMyProfile(User us)
        {
            var existingUser = await db.user.FindAsync(us.UserId);

            if (existingUser == null) {
                return;
            }
            existingUser.FirstName = us.FirstName;
            existingUser.LastName = us.LastName;
           
            existingUser.PhoneNumber = us.PhoneNumber;
            existingUser.AboutEmployee = us.AboutEmployee;
            existingUser.Email = us.Email;
            existingUser.DateOfBirth = us.DateOfBirth; 
            existingUser.Address = us.Address;
            if (!string.IsNullOrEmpty(us.ProfilePicture)) {
                existingUser.ProfilePicture = us.ProfilePicture;
            }

            await db.SaveChangesAsync();
        }
    }
}
