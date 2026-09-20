using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.ViewModel;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers
{
    public class EarningController : Controller
    {
        private readonly IEarningService service;

        public EarningController(IEarningService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> AddEarning()
        {
         
            EarningViewModel vm = new EarningViewModel();

            vm.Earnings = await service.FetchEarningList();
            vm.EarningList = await service.FetchEarningType();
            vm.DepartmentList = await service.FetchDepartment();
            vm.DesignationList = await service.FetchDesignation();

            return View(vm);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddEarning(EarningViewModel e)
        {
            await service.AddEarning(e.Earning);
            TempData["Success"] = "Earning added successfully";
            return RedirectToAction("AddEarning");

        }


        public async Task<IActionResult> AddEarningType()
        {
            ViewBag.EarningTypeList = await service.FetchEarningTypeList();
            return View(new EarningType());
        }

        [HttpPost]
        public async Task<IActionResult> AddEarningType(EarningType e)
        {
            await service.AddEarningType(e);
            TempData["Success"] = "Earning type added successfully";
            return RedirectToAction("AddEarningType");
        }

        public async Task<IActionResult> DeleteEarningType(int id)
        {
            await service.DeleteEarningType(id);
            TempData["Success"] = "Earning type deleted successfully";
            return RedirectToAction("AddEarningType");
        }

        public async Task<IActionResult> FetchEarningTypeById()
        {
            var id = Convert.ToInt32(Request.Query["id"]);
            var earningType = await service.FetchEarningTypeById(id);
            if (earningType == null)
            {
                return NotFound();
            }
            return Json(earningType);
        }

        [HttpPost]
        public async Task<IActionResult> EditEarningType(EarningType e)
        {
            await service.UpdateEarningType(e);
            TempData["Success"] = "Earning Type Updated Successfully";
            return RedirectToAction("AddEarningType");   
        }


        public async Task<IActionResult> DeleteEarning(int id)
        {
            await service.DeleteEarning(id);
            TempData["Success"] = "Earning deleted successfully";
            return RedirectToAction("AddEarning");
        }

    }
}
