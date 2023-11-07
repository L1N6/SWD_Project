using Microsoft.AspNetCore.Mvc;
using SWD392_EventManagement.IRepository.Repository;
using SWD392_EventManagement.IRepository;
using SWD392_EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace SWD392_EventManagement.Controllers
{
    public class EventsController : Controller
    {
        private EventIRepository _eventRepository = new EventRepository();
        private Swd392Project2Context context = new Swd392Project2Context();

        public List<Event> Events { get; set; }

        public IActionResult Index()
        {
            Events = _eventRepository.GetAll();
            ViewBag.EventsList = Events;
            return View(Events);
        }

        [HttpPost]
        public IActionResult Search(string? searchName, DateTime? startDate, DateTime? endDate)
        {
            EventFilter eventFilter = new EventFilter()
            {
                name = searchName,
                startDate = startDate,
                endDate = endDate
            };
            Events = _eventRepository.GetAllByFilter(eventFilter);

            ViewBag.EventsList = Events;
            return View(Events);
        }
        
        public IActionResult Detail(int id)
        {
            Event detailEvent = _eventRepository.GetOne(id);
            List<Comment> comments = context.Comments.Where(c => c.EventId == id).Include(c => c.Account).ToList();
            ViewBag.DetailEvent = detailEvent;
            ViewBag.CommentList = comments;
            return View(detailEvent);
        }

    }
}
