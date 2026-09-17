using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IDesignationService
    {
        public Task AddDesignation(Designation designation);

        public Task RemoveDesignation(int id);

        public Task UpdateDesignation(Designation designation);

        public Task<Designation>getDesignationById(int id);

        public Task<List<Designation>> GetAllDesignations();

       

    }
}
