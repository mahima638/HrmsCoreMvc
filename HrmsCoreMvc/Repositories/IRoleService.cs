using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IRoleService
    {

        public void AddRole(Role role);

        public List<Role> GetAllRole();
    }
}
