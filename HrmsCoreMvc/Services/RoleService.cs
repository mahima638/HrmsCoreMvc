using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;

namespace HrmsCoreMvc.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext db;
        public RoleService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddRole(Role role)
        {
            db.Add(role);
            db.SaveChanges();
        }

        public List<Role> GetAllRole()
        {
            return db.role.ToList();
        }
    }
}
