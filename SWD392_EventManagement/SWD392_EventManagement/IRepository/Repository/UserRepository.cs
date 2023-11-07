using SWD392_EventManagement.DAO;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.IRepository.Repository
{
    public class UserRepository : IUserRepository
    {

        public UserDAO userDAO = new UserDAO();  

        public bool ChangePass(int accountID, string newPass, string confirmNewPass)
        {

            try
            {
                if (!newPass.Equals(confirmNewPass)) {
                    throw new Exception("The pass and confirm pass is not same");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return true;
        }

        public Account FindUserById(int id)
        {
            try
            {
                var account = userDAO.FindUserById(id);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Account Login(string email, string pass)
        {
            try {
                var account = userDAO.Login(email, pass);
                if (account == null) {
                    throw new Exception("User không tồn tại");
                }
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
        public bool ChangeAvatar(int id, string nameFile) {
            try
            {
                userDAO.ChangeAvatar(id, nameFile);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
