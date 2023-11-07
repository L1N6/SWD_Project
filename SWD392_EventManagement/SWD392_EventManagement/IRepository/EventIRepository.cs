using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.IRepository
{
    public interface EventIRepository
    {
        List<Event> GetAll();
        Event GetOne(int id);
        List<Event> GetAllByFilter(EventFilter eventFilter);      
    }
}
