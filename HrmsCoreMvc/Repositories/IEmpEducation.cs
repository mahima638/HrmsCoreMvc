using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IEmpEducation
    {
        public Task AddEmpEducation(EmpEducation emped);

        public Task DeleteEmpEducation(int id);

        public Task<List<EmpEducation>> GetEmpEducation();

        public Task<EmpEducation> GetEmpEducationById(int id);

        public Task EditEmpEducation(EmpEducation emped);

        Task<List<EmpEducation?>> GetEducationByUserId(int userId);


    }
}