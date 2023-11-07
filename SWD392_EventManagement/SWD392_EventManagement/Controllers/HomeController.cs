using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.IRepository;
using SWD392_EventManagement.IRepository.Repository;
using SWD392_EventManagement.Models;
using System.Diagnostics;

namespace SWD392_EventManagement.Controllers
{
    public class HomeController : Controller
    {
        private EventIRepository _eventRepository = new EventRepository();
        private readonly ILogger<HomeController> _logger;
        public Swd392Project2Context context = new Swd392Project2Context();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<Event> Events { get; set; }

        public IActionResult Index()
        {
            Events = _eventRepository.GetAll();
            ViewBag.EventsList = Events;
            return View(Events);
        }

        [HttpPost]
        public IActionResult Search(string searchName, DateTime startDate, DateTime endDate)
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}