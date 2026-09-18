using HrmsCoreMvc.Models.Events;
namespace HrmsCoreMvc.Repositories
{
    public interface IEventTypeService
    {
        public Task<List<EventType>> GetAllEventTypes();
        public Task<string> AddEventType(EventType eventType);
        public Task<string> UpdateEventType(EventType eventType);
        public Task<string> DeleteEventType(int eventTypeId);
        public Task<List<EventType>> SearchEventTypes(string searcheventtype);
    }
}
