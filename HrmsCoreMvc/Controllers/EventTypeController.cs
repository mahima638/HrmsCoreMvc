using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;
namespace HrmsCoreMvc.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly IEventTypeService cs;
        public EventTypeController(IEventTypeService ets)
        {
            cs = ets;
        }

        public async Task<IActionResult> GetAllEventTypes()
        {
            var eventTypes = await cs.GetAllEventTypes();
            return View("~/Views/Event/GetAllEventTypes.cshtml", eventTypes);
        }

        public async Task<IActionResult> AddEventType(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                await cs.AddEventType(eventType);
                TempData["SuccessMessage"] = "Event Type Added Successfully!";
                return RedirectToAction("GetAllEventTypes");
            }
            return View(eventType);
        }

        public async Task<IActionResult> UpdateEventType(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateEventType(eventType);
                TempData["SuccessMessage"] = "Event Type Updated Successfully!";
                return RedirectToAction("GetAllEventTypes");
            }
            return View(eventType);
        }

        public async Task<IActionResult> DeleteEventType(int eventTypeId)
        {
            await cs.DeleteEventType(eventTypeId);
            TempData["SuccessMessage"] = "Event Type Deleted Successfully!";
            return RedirectToAction("GetAllEventTypes");
        }

        public async Task<IActionResult> SearchEventTypes(string searcheventtype)
        {
            var eventTypes = await cs.SearchEventTypes(searcheventtype);
            return View("GetAllEventTypes", eventTypes);
        }
    }
}
