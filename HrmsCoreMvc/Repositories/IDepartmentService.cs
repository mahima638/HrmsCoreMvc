using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IDepartmentService
    {

        public void AddDepartment(Departments dept);

        public void UpdateDepartment(Departments dept);

        public List<Departments> GetDepartments();

        public void DeleteDepartment(int id);
      
        public Departments GetDepartmentById(int id);

    }
}
