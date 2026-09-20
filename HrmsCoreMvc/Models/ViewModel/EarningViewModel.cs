using HrmsCoreMvc.Models.PayRoll;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Models.ViewModel
{
    public class EarningViewModel
    {
        public Earning Earning { get; set; } = new();

        public List<Earning> Earnings { get; set; } = new();

        public List<SelectListItem> EarningList { get; set; } = new();
        public List<SelectListItem> DepartmentList { get; set; } = new();
        public List<SelectListItem> DesignationList { get; set; } = new();
    }
}
