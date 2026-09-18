using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IDepartmentService
    {

        public Task AddDepartment(Departments dept);

        public Task UpdateDepartment(Departments dept);

        public Task<List<Departments>> GetDepartments();

        public Task DeleteDepartment(int id);
      
        public Task<Departments> GetDepartmentById(int id);

       

    }
}
