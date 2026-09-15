using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IRoleService
    {

        public void AddRole(Role role);

        public List<Role> GetAllRole();


        public void DeleteRole(int id);

        public void EditRole(Role role);

        public Role GetRoleById(int id);
    }
}
