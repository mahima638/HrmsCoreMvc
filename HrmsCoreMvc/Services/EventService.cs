using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext db;

        public EventService(ApplicationDbContext cs)
        {
            db = cs;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await db.events.ToListAsync();
        }

        public async Task<string> AddEvent(Event eventObj)
        {
            db.events.Add(eventObj);
            await db.SaveChangesAsync();
            return "Event added successfully";
        }

        public async Task<string> UpdateEvent(Event eventObj)
        {
            db.events.Update(eventObj);
            await db.SaveChangesAsync();
            return "Event updated successfully";
        }

        public async Task<string> DeleteEvent(int eventId)
        {
            var eventObj = await db.events.FindAsync(eventId);
            if (eventObj != null)
            {
                db.events.Remove(eventObj);
                await db.SaveChangesAsync();
                return "Event deleted successfully";
            }
            return "Event not found";
        }

        public async Task<List<Event>> SearchEvents(string searchevent)
        {
            return await db.events.Where(e => e.Title.Contains(searchevent)).ToListAsync();
        }
    }
}
