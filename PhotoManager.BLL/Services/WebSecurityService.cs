using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public static class WebSecurityService
    {

        public static User GetUser(string username)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Users.GetUserByName(username);
            }
        }
        public static string GetUserNameById(int userId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Users.GetUserById(userId).UserName;
            }
        }

        public static User GetCurrentUser()
        {
            return GetUser(CurrentUserName);
        }

        public static int GetCurrentUserId()
        {
            return GetUser(CurrentUserName).UserId;
        }

        public static bool IsPayedUser()
        {
            return GetUser(CurrentUserName) is PayedUser;
        }

        public static bool Login(string userName, string password, bool persistCookie = false)
        {
            return WebMatrix.WebData.WebSecurity.Login(userName, password, persistCookie);
        }

        public static string CreateUserAndAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebMatrix.WebData.WebSecurity.CreateUserAndAccount(userName, password, new {Discriminator = "FreeUser"}, requireConfirmationToken);
        }

        public static int GetUserId(string userName)
        {
            return WebMatrix.WebData.WebSecurity.GetUserId(userName);
        }

        public static void Logout()
        {
            WebMatrix.WebData.WebSecurity.Logout();
        }

        public static bool IsAuthenticated { get { return WebMatrix.WebData.WebSecurity.IsAuthenticated; } }

        public static string CurrentUserName { get { return WebMatrix.WebData.WebSecurity.CurrentUserName; } }
    }
}
