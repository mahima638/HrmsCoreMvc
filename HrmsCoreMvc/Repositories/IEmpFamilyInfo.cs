using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IEmpFamilyInfo
    {
        public Task AddEmpFamilyInfo(EmpFamilyInfo empfam);

        public Task DeleteEmpFamilyInfo(int id);

        public Task<List<EmpFamilyInfo>> GetEmpFamilyInfo();

        public Task<EmpFamilyInfo> GetEmpFamilyInfoById(int id);

        public Task EditEmpFamilyInfo(EmpFamilyInfo empfam);
        Task<List<EmpFamilyInfo?>> GetFamilyInfoByUserId(int userId);
    }
}
