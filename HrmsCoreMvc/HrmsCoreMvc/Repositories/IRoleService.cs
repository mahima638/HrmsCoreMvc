using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IRoleService
    {

        public Task AddRole(Role role);

        public Task<List<Role>> GetAllRole();



        public Task DeleteRole(int id);

        public Task EditRole(Role role);

        public Task<Role> GetRoleById(int id);

    }
}
