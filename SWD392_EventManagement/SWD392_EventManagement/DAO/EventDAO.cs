using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.DAO
{
    public class EventDAO
    {
        private readonly Swd392Project2Context context = new Swd392Project2Context();

        public List<Event> GetAll()
        {
            List<Event> list = new List<Event>();
            try
            {
                list = context.Events.Include(e => e.EventDetails).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        public List<Event> GetAllByFilter(EventFilter eventFilter)
        {
            List<Event> list = new List<Event>();
            try
            {
                var query = context.Events.AsQueryable();

                if (!eventFilter.name.IsNullOrEmpty())
                {
                    query = query.Where(e => e.Name.Contains(eventFilter.name));
                }

                if (eventFilter.startDate != null)
                {
                    query = query.Where(e => e.StartDate.Date == eventFilter.startDate.Value.Date);
                }

                if (eventFilter.endDate != null)
                {
                    query = query.Where(e => e.EndDate.Date == eventFilter.endDate.Value.Date);
                }
                
                list = query.Include(e => e.EventDetails).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }

        public Event GetOne(int id)
        {
            Event _event = new Event();
            try
            {
                _event = context.Events
                    .Include(e => e.EventDetails)
                    .Include(e => e.Category)
                    .Include(e => e.Sponsors)
                    .Include(e => e.Account)
                    .FirstOrDefault(e => e.EventId == id);
            }
            catch (Exception)
            {

                throw;
            }
            return _event;
        }
    }
}
