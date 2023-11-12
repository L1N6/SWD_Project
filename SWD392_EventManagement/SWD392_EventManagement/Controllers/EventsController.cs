using Microsoft.AspNetCore.Mvc;
using SWD392_EventManagement.IRepository.Repository;
using SWD392_EventManagement.IRepository;
using SWD392_EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace SWD392_EventManagement.Controllers
{
    public class EventsController : Controller
    {
        private EventIRepository _eventRepository = new EventRepository();
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private Prn221ProjectContext context = new Prn221ProjectContext();

        public List<Event> Events { get; set; }

        public IActionResult Index(string? searchName, DateTime? startDate, DateTime? endDate)
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            List<Connection> connections = new List<Connection>();
            connections = context.Connections.ToList();
            EventFilter eventFilter = new EventFilter()
            {
                name = searchName,
                startDate = startDate,
                endDate = endDate
            };
            Events = _eventRepository.GetAllByFilter(eventFilter);
            ViewBag.EventsList = Events;
            ViewBag.AccountId = idUser;
            ViewBag.Care = connections;
            return View(Events);
        }
        
        public IActionResult Detail(int id)
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            Event detailEvent = _eventRepository.GetOne(id);
            List<Comment> comments = context.Comments.Where(c => c.EventId == id).Include(c => c.Account).ToList();
            ViewBag.DetailEvent = detailEvent;
            ViewBag.CommentList = comments;
            return View(detailEvent);
        }

        public IActionResult ProfileEvent()
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            List<Event> events = _eventRepository.GetAllByFilter(new EventFilter() { accountId = idUser });
            ViewBag.ListProfileEvent = events;
            return View(events);
        }
        public IActionResult Create()
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Description, StartDate, EndDate,  Location, CategoryId, AccountId")] Event createEvent, 
            [Bind("Image")] EventDetail createEventDetail, [Bind(" InforSponsor, UnitSponsor")] Sponsor sponsor, string SponsorName)
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            createEvent.AccountId = idUser;
            ViewBag.Categories = context.Categories.ToList();

            context.Add(createEvent);
            context.SaveChanges();

            createEventDetail.EventId = context.Events.OrderByDescending(e => e.EventId).FirstOrDefault().EventId;
            context.Add(createEventDetail);
            context.SaveChanges();

            sponsor.EventId = context.Events.OrderByDescending(e => e.EventId).FirstOrDefault().EventId;
            sponsor.Name = SponsorName;
            context.Add(sponsor);
            context.SaveChanges();
            return RedirectToAction("ProfileEvent", "Events");
        }


        public IActionResult CareEvent(int id)
        {
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            Connection connection = new Connection()
            {
                EventId = id,
                AccountId = idUser,
                JoinOrCare = true
            };
            context.Add(connection);
            context.SaveChanges();
            return RedirectToAction("Index", "Events");
        }


    }
}
