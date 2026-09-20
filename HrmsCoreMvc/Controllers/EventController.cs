using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Services;
using HrmsCoreMvc.Data;
using System.Threading.Tasks;

namespace HrmsCoreMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService cs;
        private readonly IEventTypeService ets;

        public EventController(IEventService cs, IEventTypeService ets)
        {
            this.cs = cs;
            this.ets = ets;
        }

        public async Task<IActionResult> GetAllEvents()
        {
            var events = await cs.GetAllEvents();
            ViewBag.EventTypes = await ets.GetAllEventTypes();
            return View("GetAllEvents", events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event eventObj)
        {
            await cs.AddEvent(eventObj);
            TempData["SuccessMessage"] = "Event added successfully!";
            return RedirectToAction("GetAllEvents");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int eventId)
        {
            var eventObj = (await cs.GetAllEvents()).FirstOrDefault(x => x.EventId == eventId);
            if (eventObj == null)
            {
                return NotFound();
            }
            ViewBag.EventTypes = await ets.GetAllEventTypes();
            return View(eventObj);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(Event eventObj)
        {
            if (ModelState.IsValid)
            {
                await cs.UpdateEvent(eventObj);
                TempData["SuccessMessage"] = "Event Updated Successfully!";
                return RedirectToAction("Holidays");
            }
            ViewBag.EventTypes = await ets.GetAllEventTypes();
            return View("UpdateEvent", eventObj);
        }

        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            await cs.DeleteEvent(eventId);
            TempData["SuccessMessage"] = "Event Deleted Successfully!";
            return RedirectToAction("Holidays");
        }

        public async Task<IActionResult> Holidays()
        {
            var events = await cs.GetAllEvents();
            ViewBag.EventTypes = await ets.GetAllEventTypes();
            return View("Holidays", events);
        }

        [HttpPost]
        public async Task<IActionResult> AddHoliday(Event eventObj)
        {
            await cs.AddEvent(eventObj);
            TempData["SuccessMessage"] = "Holiday added successfully!";
            return RedirectToAction("Holidays");
        }
        public async Task<IActionResult> SearchEvents(string searchevent)
        {
            var events = await cs.SearchEvents(searchevent);
            return View("GetAllEvents", events);
        }
        
    }
}
