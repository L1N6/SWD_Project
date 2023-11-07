using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace SWD392_EventManagement.DAO
{
    public class UserDAO 
    {
      
        private readonly Swd392Project2Context _context = new Swd392Project2Context();

 
        public Account Login(string email, string pass) {
            var login = _context.Accounts.Where(x => x.Password.Equals(pass) && x.Email.Equals(email)).FirstOrDefault();
            return login;
        }

        public Account FindUserById(int id)
        {
            var Account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
            return Account;
        }

        public void UpdateAccount(Account Account)
        {
            Account cus_ = _context.Accounts.SingleOrDefault(p => p.AccountId == Account.AccountId);
            _context.Entry(cus_).State = EntityState.Detached;
            _context.Accounts.Update(Account);
            _context.SaveChanges();
        }

        public void ChangeAvatar(int id, string nameFile) {
            var  acc = FindUserById(id);
            acc.Avatar = nameFile;
            _context.Update(acc);
            _context.SaveChanges();
        }

        public void ChangePass(int accountID, string newPass) {
            var acc = FindUserById(accountID);
            acc.Password = newPass;
            _context.Update(acc);
            _context.SaveChanges();
        }
    }
     
}
