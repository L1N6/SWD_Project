using Microsoft.AspNetCore.Mvc;

namespace SWD392_EventManagement.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
