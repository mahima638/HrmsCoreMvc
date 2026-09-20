using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IUserService
    {
        public Task AddEmployee(User us);

        public Task DeleteEmployee(int id);

        public Task<List<User>> getEmployees();

        public Task UpdateEmployee(User us);

        public Task<User>GetEmpById(int id);

        Task UpdateMyProfile(User us);
    }
}
