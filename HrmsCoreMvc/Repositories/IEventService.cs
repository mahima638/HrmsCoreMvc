using HrmsCoreMvc.Models.Events;
namespace HrmsCoreMvc.Repositories
{
    public interface IEventService
    {
        public List<Event> GetAllEvents();
        public string AddEvent(Event eventObj);
        public string UpdateEvent(Event eventObj);
        public string DeleteEvent(int eventId);
        public List<Event> SearchEvents(string searchevent);
    }
}
