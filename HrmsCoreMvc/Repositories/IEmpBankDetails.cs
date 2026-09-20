using HrmsCoreMvc.Models;

namespace HrmsCoreMvc.Repositories
{
    public interface IEmpBankDetails
    {
        public Task AddEmpBankDetails(EmpBankDetails empbd);

        public Task DeleteEmpBankDetails(int id);

        public Task<List<EmpBankDetails>> GetEmpBankDetails();

        public Task<EmpBankDetails> GetEmpBankDetailsById(int id);

        public Task EditEmpBankDetails (EmpBankDetails empbd);
        Task<EmpBankDetails?> GetBankDetailsByUserId(int userId);
    }
}
