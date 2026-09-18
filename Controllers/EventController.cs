using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;

namespace HrmsCoreMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService cs;

        public EventController(IEventService es)
        {
            cs = es;
        }

        public IActionResult GetAllEvents()
        {
            var events = cs.GetAllEvents();
            return View(events);
        }

        public IActionResult AddEvent(Event eventObj)
        {
            if (ModelState.IsValid)
            {
                cs.AddEvent(eventObj);
                TempData["SuccessMessage"] = "Event Added Successfully!";
                return RedirectToAction("GetAllEvents");
            }
            return View(eventObj);
        }

        public IActionResult UpdateEvent(Event eventObj)
        {
            if (ModelState.IsValid)
            {
                cs.UpdateEvent(eventObj);
                TempData["SuccessMessage"] = "Event Updated Successfully!";
                return RedirectToAction("GetAllEvents");
            }
            return View(eventObj);
        }

        public IActionResult DeleteEvent(int eventId)
        {
            cs.DeleteEvent(eventId);
            TempData["SuccessMessage"] = "Event Deleted Successfully!";
            return RedirectToAction("GetAllEvents");
        }

        public IActionResult SearchEvents(string searchevent)
        {
            var events = cs.SearchEvents(searchevent);
            return View("GetAllEvents", events);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
