using HrmsCoreMvc.Models.Events;
namespace HrmsCoreMvc.Repositories
{
    public interface IEventService
    {
        public Task<List<Event>> GetAllEvents();
        public Task<string> AddEvent(Event eventObj);
        public Task<string> UpdateEvent(Event eventObj);
        public Task<string> DeleteEvent(int eventId);
        public Task<List<Event>> SearchEvents(string searchevent);
    }
}
