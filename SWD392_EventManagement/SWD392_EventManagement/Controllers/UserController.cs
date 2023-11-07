using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.Models;
using System.Security.Claims;

namespace SWD392_EventManagement.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            //string username = Account.UserName ?? string.Empty;
            //string password = Account.Password ?? string.Empty;

            //var user = _context.Accounts.FirstOrDefault(a => a.UserName == username && a.Password == password);

            //if (user != null)
            //{
            //    var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, user.UserName)
            //        };


            //    if (user.Type)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, "admin"));

            //    }

            //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //    var principal = new ClaimsPrincipal(identity);

            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        principal,
            //        new AuthenticationProperties
            //        {
            //            IsPersistent = true,
            //            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            //        });
            //    if (user.Type)
            //    {
            //        return RedirectToPage("/Admin/AdminPage");
            //    }
            //    else
            //    {
            //        return RedirectToPage("/Index");
            //    }
            return View();
        }
    }
}
