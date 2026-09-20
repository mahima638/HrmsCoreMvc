using HrmsCoreMvc.Models.PayRoll;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Repositories
{
    public interface IEarningService
    {
        public Task AddEarningType(EarningType e);
        public Task AddEarning(Earning earning);

         Task<List<SelectListItem>> FetchEarningType();

        Task<List<SelectListItem>> FetchDepartment();

        Task<List<SelectListItem>> FetchDesignation();

        Task<List<Earning>> FetchEarningList();

        Task<List<EarningType>> FetchEarningTypeList();

        Task<Earning?> FetchEarningById(int id);

        Task<EarningType?> FetchEarningTypeById(int id);

        public Task UpdateEarning(Earning earning);
        public Task UpdateEarningType(EarningType earningType);

        public Task DeleteEarning(int id);
        public Task DeleteEarningType(int id);
    }
}
