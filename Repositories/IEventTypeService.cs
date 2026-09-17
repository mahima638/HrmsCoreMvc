using HrmsCoreMvc.Models.Events;
namespace HrmsCoreMvc.Repositories
{
    public interface IEventTypeService
    {
        public List<EventType> GetAllEventTypes();
        public string AddEventType(EventType eventType);
        public string UpdateEventType(EventType eventType);
        public string DeleteEventType(int eventTypeId);
        public List<EventType> SearchEventTypes(string searcheventtype);
    }
}
