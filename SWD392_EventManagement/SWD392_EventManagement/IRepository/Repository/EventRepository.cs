using SWD392_EventManagement.DAO;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.IRepository.Repository
{
    public class EventRepository : EventIRepository
    {
        EventDAO _eventDAO = new EventDAO();
        public List<Event> GetAll()
        {
            List<Event> list = new List<Event>();
            list = _eventDAO.GetAll();
            return list;
        }

        public List<Event> GetAllByFilter(EventFilter eventFilter)
        {
            List<Event> list = new List<Event>();
            list = _eventDAO.GetAllByFilter(eventFilter);
            return list;
        }

        public Event GetOne(int id)
        {
            Event detailEvent = _eventDAO.GetOne(id);
            return detailEvent;
        }
    }
}
