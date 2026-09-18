using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HrmsCoreMvc.Services
{
    public class Departmentservice : IDepartmentService
    {
        private readonly ApplicationDbContext db;
        public Departmentservice(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddDepartment(Departments dept)
        {
            db.Add(dept);
            await db.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var dept = db.department.Find(id);
            if (dept != null)
            {
                db.department.Remove(dept);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Departments> GetDepartmentById(int id)
        {
            return db.department.Find(id);
        }

        public async Task<List<Departments>> GetDepartments()
        {
            return await db.department.ToListAsync();
        }

        public async Task UpdateDepartment(Departments dept)
        { 
            db.Update(dept);
            await db.SaveChangesAsync();
        }
    }
}
