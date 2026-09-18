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

        public IActionResult GetAllEventTypes()
        {
            var eventTypes = cs.GetAllEventTypes();
            return View(eventTypes);
        }

        public IActionResult AddEventType(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                cs.AddEventType(eventType);
                TempData["SuccessMessage"] = "Event Type Added Successfully!";
                return RedirectToAction("GetAllEventTypes");
            }
            return View(eventType);
        }

        public IActionResult UpdateEventType(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateEventType(eventType);
                TempData["SuccessMessage"] = "Event Type Updated Successfully!";
                return RedirectToAction("GetAllEventTypes");
            }
            return View(eventType);
        }

        public IActionResult DeleteEventType(int eventTypeId)
        {
            cs.DeleteEventType(eventTypeId);
            TempData["SuccessMessage"] = "Event Type Deleted Successfully!";
            return RedirectToAction("GetAllEventTypes");
        }

        public IActionResult SearchEventTypes(string searcheventtype)
        {
            var eventTypes = cs.SearchEventTypes(searcheventtype);
            return View("GetAllEventTypes", eventTypes);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
