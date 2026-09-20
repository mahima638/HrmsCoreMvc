using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.ViewModel;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class DeductionController : Controller
    {
        private readonly IDeductionService service;
        public DeductionController(IDeductionService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddDeduction()
        {
            DeductionViewModel vm = new DeductionViewModel();
            vm.Deductions = await service.FetchDeductionList();
            vm.DeductionList = await service.FetchDeductionType();
            vm.DepartmentList = await service.FetchDepartment();
            vm.DesignationList = await service.FetchDesignation();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddDeduction(DeductionViewModel d)
        {
            await service.AddDeduction(d.Deduction);
            TempData["Success"] = "Deduction added successfully";
            return RedirectToAction("AddDeduction");
  
        }

        public async Task<IActionResult> AddDeductionType()
        {
            ViewBag.DeductionTypeList = await service.FetchDeductionTypeList();
            return View(new DeductionType());
        }


        [HttpPost]
        public async Task<IActionResult> AddDeductionType(DeductionType d)
        {
            await service.AddDeductionType(d);
            TempData["Success"] = "Deduction type added successfully";
            return RedirectToAction("AddDeductionType");
        }

        

        [HttpPost]
        public async Task<IActionResult> EditDeduction(DeductionViewModel vm)
        {
              await service.UpdateDeduction(vm.Deduction);
              TempData["Success"] = "Deduction updated successfully.";
              return RedirectToAction("AddDeduction");
            

            vm.Deductions = await service.FetchDeductionList();
            vm.DeductionList = await service.FetchDeductionType();
            vm.DepartmentList = await service.FetchDepartment();
            vm.DesignationList = await service.FetchDesignation();

            return View("AddDeduction", vm);
        }

        public async Task<IActionResult> DeleteDeduction(int id)
        {
            await service.DeleteDeduction(id);

            TempData["Success"] = "Deduction deleted successfully";
            return RedirectToAction("AddDeduction");
        }

        [HttpPost]
        public async Task<IActionResult> EditDeductionType(DeductionType deductionType)
        {
            
                await service.UpdateDeductionType(deductionType);

                TempData["Success"] = "Deduction Type updated successfully.";
                return RedirectToAction("AddDeductionType");
            

            ViewBag.DeductionTypeList = await service.FetchDeductionTypeList();
            return View("AddDeductionType", deductionType);
        }

        public async Task<IActionResult> DeleteDeductionType(int id)
        {
            await service.DeleteDeductionType(id);
            TempData["Success"] = "Deduction type deleted successfully";
            return RedirectToAction("AddDeductionType");
        }
    }
}
