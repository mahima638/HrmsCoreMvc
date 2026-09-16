using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IUserService
    {
        public void AddEmployee(User us);

        public void DeleteEmployee(int id);

        public List<User> getEmployees();

        public void UpdateEmployee(User us);

        public User GetEmpById(int id);
    }
}
