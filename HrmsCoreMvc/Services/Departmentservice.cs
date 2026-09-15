using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;

namespace HrmsCoreMvc.Services
{
    public class Departmentservice : IDepartmentService
    {
        private readonly ApplicationDbContext db;
        public Departmentservice(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddDepartment(Departments dept)
        {
            db.Add(dept);
            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var dept = db.department.Find(id);
            if (dept != null)
            {
                db.department.Remove(dept);
                db.SaveChanges();
            }
        }

        public Departments GetDepartmentById(int id)
        {
            return db.department.Find(id);
        }

        public List<Departments> GetDepartments()
        {
            return db.department.ToList();
        }

        public void UpdateDepartment(Departments dept)
        { 
            db.Update(dept);
            db.SaveChanges();
        }
    }
}
