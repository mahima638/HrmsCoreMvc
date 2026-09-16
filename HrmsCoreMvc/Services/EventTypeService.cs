using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly ApplicationDbContext db;
        public EventTypeService(ApplicationDbContext cs)
        {
            db = cs;
        }

        public async Task<List<EventType>> GetAllEventTypes()
        {
            return await db.eventtypes.ToListAsync();
        }

        public async Task<string> AddEventType(EventType eventType)
        {
            db.eventtypes.Add(eventType);
            await db.SaveChangesAsync();
            return "Event type added successfully";
        }

        public async Task<string> UpdateEventType(EventType eventType)
        {
            db.eventtypes.Update(eventType);
            await db.SaveChangesAsync();
            return "Event type updated successfully";
        }

        public async Task<string> DeleteEventType(int eventTypeId)
        {
            var eventType = await db.eventtypes.FindAsync(eventTypeId);
            if (eventType != null)
            {
                db.eventtypes.Remove(eventType);
                await db.SaveChangesAsync();
                return "Event type deleted successfully";
            }
            return "Event type not found";
        }

        public async Task<List<EventType>> SearchEventTypes(string searcheventtype)
        {
            return await db.eventtypes
                .Where(et => et.EventTypeName.Contains(searcheventtype))
                .ToListAsync();
        }

    }
}
