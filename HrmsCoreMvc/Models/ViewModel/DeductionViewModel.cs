using HrmsCoreMvc.Models.PayRoll;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrmsCoreMvc.Models.ViewModel
{
    public class DeductionViewModel
    {
        public Deduction Deduction { get; set; } = new();

       
        public List<Deduction> Deductions { get; set; } = new();

        
        public List<SelectListItem> DeductionList { get; set; } = new();

        public List<SelectListItem> DepartmentList { get; set; } = new();

        public List<SelectListItem> DesignationList { get; set; } = new();
    }
}
