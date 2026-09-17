using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IDesignationService
    {
        public void AddDesignation(Designation designation);

        public void RemoveDesignation(int id);

        public void UpdateDesignation(Designation designation);

        public Designation getDesignationById(int id);

        public List<Designation> GetAllDesignations();

       

    }
}
