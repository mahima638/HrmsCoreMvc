using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrmsCoreMvc.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddEmployee(User us)
        {
            db.Add(us);
            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            db.Remove(id);
            db.SaveChanges();
        }

        public User GetEmpById(int id)
        {
            return db.user.Find(id);
           
        }

        public List<User> getEmployees()
        {
            return db.user.ToList();
        }

        public void UpdateEmployee(User us)
        {
            db.Update(us);
            db.SaveChanges();
        }
    }
}
