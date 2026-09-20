using HrmsCoreMvc.Models.PayRoll;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Repositories
{
    public interface IDeductionService
    {
        public Task AddDeductionType(DeductionType d);
        public Task AddDeduction(Deduction deduction); 
        Task<List<SelectListItem>> FetchDeductionType();
        Task<List<SelectListItem>> FetchDepartment();
        Task<List<SelectListItem>> FetchDesignation();  
        Task<List<Deduction>> FetchDeductionList();
        Task<List<DeductionType>> FetchDeductionTypeList();
        Task<Deduction?> FetchDeductionById(int id);
        Task<DeductionType?> FetchDeductionTypeById(int id);     
        public Task UpdateDeduction(Deduction deduction);
        public Task UpdateDeductionType(DeductionType deductionType);
        public Task DeleteDeduction(int id);
        public Task DeleteDeductionType(int id);
    }
}
