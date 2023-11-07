using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.Models;
using System.Security.Claims;
using SWD392_EventManagement.IRepository;
using SWD392_EventManagement.IRepository.Repository;

namespace SWD392_EventManagement.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public UserController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        IUserRepository userRepository = new UserRepository();
        EventIRepository eventIRepository = new EventRepository();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var account = userRepository.Login(email, password);
                if (account.RoleId == 1)
                {

                }
                else {
                    HttpContext.Session.SetString("User", $"{account.AccountId}");
                    HttpContext.Session.SetString("UserName", $"{account.FullName}");
                    return RedirectToAction("UserProfile", "User");
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }

        public ActionResult UserProfile() {
            var user = new Account();
            if (!CheckUserSession()) {
                TempData["ErrorMessage"] = "Need To Login";
                return RedirectToAction("Login", "User");
            }
            int idUser = int.Parse(HttpContext.Session.GetString("User"));
            TempData["UserName"] = (HttpContext.Session.GetString("UserName"));
            try
            {
                 user = userRepository.FindUserById(idUser);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(user);
        }

        public ActionResult AddAvatar(IFormFile uploadedFile) {
            try
            {
                string nameFile = "";
                if (uploadedFile != null)
                {
                    nameFile = UploadFile(uploadedFile);
                }
                int idUser = int.Parse(HttpContext.Session.GetString("User"));
                userRepository.ChangeAvatar(idUser, nameFile);
            }
            catch (Exception)
            {

                return RedirectToAction("Privacy", "Home");
            }
         
            return RedirectToAction("UserProfile", "User");
        }

        public ActionResult ChangePass(string newPassword, string confirmNewPass)
        {
            try
            {
                int idUser = int.Parse(HttpContext.Session.GetString("User"));
                userRepository.ChangePass(idUser, newPassword, confirmNewPass);
                TempData["Mess"] = "Update Success";
            }
            catch (Exception ex)
            {
                TempData["Mess"] = ex.Message;
            }

            return RedirectToAction("UserProfile", "User");
        }

        public String UploadFile(IFormFile uploadedFile)
        {
            var filePath = "";
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                var uploadPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\wwwroot", "SaveFile");
                filePath = Path.Combine(uploadPath, uploadedFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(stream);
                }
            }

            return uploadedFile.FileName;
        }

        public bool CheckUserSession()
        {
            var strUserId = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(strUserId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "User");
        }




    }
}
