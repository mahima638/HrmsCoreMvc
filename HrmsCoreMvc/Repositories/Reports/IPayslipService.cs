using HrmsCoreMvc.Models.Reports;

namespace HrmsCoreMvc.Repositories.Reports
{
    public interface IPayslipService
    {
        Task<List<PayslipViewModel>> fetchPayslips();

        Task<List<PayslipViewModel>> sortPayslips(string? sortMonth, string? sortType);

    }
}
