using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Termination;
namespace HrmsCoreMvc.Repositories.Terminations
{
    public interface ITerminationRepository
    {
        Task<List<Termination>> GetTerminationsAsync();
        Task<Termination> GetTerminationByIdAsync(int terminationId);
        Task<string> AddTerminationAsync(Termination terminations);
        Task<string> UpdateTerminationAsync(Termination terminations);
        Task<string> DeleteTerminationAsync(int terminationId);
        Task<List<User>> GetUsersAsync();
    }
}
