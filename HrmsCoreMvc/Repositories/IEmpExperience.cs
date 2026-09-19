using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IEmpExperience
    {
        public Task AddEmpExperience(EmpExperience empex);

        public Task DeleteEmpExperience(int id);

        public Task<List<EmpExperience>> GetEmpExperience();

        public Task<EmpExperience> GetEmpExperienceById(int id);

        public Task EditEmpExperience(EmpExperience empex);
        Task<List<EmpExperience?>> GetExperienceByUserId(int userId);
    }
}
