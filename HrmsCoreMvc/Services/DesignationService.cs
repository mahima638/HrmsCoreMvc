using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Repositories;

namespace HrmsCoreMvc.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly ApplicationDbContext db;
        public DesignationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddDesignation(Designation designation)
        {
            db.Add(designation);
            db.SaveChanges();
        }

        public List<Designation> GetAllDesignations()
        {
            return db.designation.ToList();
        }

        public Designation getDesignationById(int id)
        {
            return db.designation.Find(id);
        }

        public void RemoveDesignation(int id)
        {
            var des = db.designation.Find(id);
            db.designation.Remove(des);
            db.SaveChanges();
        }

        public void UpdateDesignation(Designation designation)
        {
            db.Update(designation); 
            db.SaveChanges();
        }
    }
}
