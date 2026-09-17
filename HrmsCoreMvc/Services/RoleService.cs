using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HrmsCoreMvc.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext db;
        public RoleService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddRole(Role role)
        {
            db.Add(role);
            await db.SaveChangesAsync();
        }


        public async Task DeleteRole(int id)
        {
            var role = await db.role.FindAsync(id);
            if (role != null) { 
            
              db.Remove(role);
                await db.SaveChangesAsync();

            }
        }

        public async Task EditRole(Role role)
        {
            db.Update(role);
            await db.SaveChangesAsync();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await  db.role.ToListAsync();
        }


        public async Task<Role?> GetRoleById(int id)
        {
          return await  db.role.FindAsync(id);
        }

    }
}
