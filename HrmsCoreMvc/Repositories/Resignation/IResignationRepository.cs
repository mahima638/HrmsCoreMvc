using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Resignation;
namespace HrmsCoreMvc.Repositories.Resignations
{
    public interface IResignationRepository
    {
        Task<List<Resignation>> GetResignationsAsync();
        Task<Resignation> GetResignationByIdAsync(int resignationId);
        Task<string> AddResignationAsync(Resignation resignation);
        Task<string> UpdateResignationAsync(Resignation resignation);
        Task<string> DeleteResignationAsync(int resignationId);
        Task<List<User>> GetUsersAsync();
        Task<List<Departments>> GetDepartmentAsync();
    }
}
