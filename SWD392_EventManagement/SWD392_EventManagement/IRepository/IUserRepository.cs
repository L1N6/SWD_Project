using Microsoft.AspNetCore.Mvc;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.IRepository
{
    public interface IUserRepository 
    {
        public Account Login(string email, string pass);
        public Account FindUserById(int id);
        public bool ChangePass(int accountID, string newPass, string confirmNewPass);
        public bool ChangeAvatar(int id, string nameFile);
    }
}
