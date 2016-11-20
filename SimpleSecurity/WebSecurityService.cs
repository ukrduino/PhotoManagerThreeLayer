using System;
using System.Data.Entity;
using SecurityModule.Entities;
using SecurityModule.Repositories;

namespace SecurityModule
{
    public static class WebSecurityService
    {
        public static void InitSecurityDataBase()
        {
            Database.SetInitializer<SecurityContext>(new InitSecurityDb());
            SecurityContext context = new SecurityContext();
            context.Database.Initialize(true);
            if (!WebMatrix.WebData.WebSecurity.Initialized)
                WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("PhotoManagerDB_3_layer",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }


        public static UserProfile GetUser(string username)
        {
            using (SecurityUnitOfWork securityUnitOfWork = new SecurityUnitOfWork())
            {
                return securityUnitOfWork.UserProfileRepository.GetUserByName(username);
            }
        }

        public static UserProfile GetCurrentUser()
        {
            return GetUser(CurrentUserName);
        }

        public static void CreateUser(UserProfile user)
        {
            UserProfile dbUser = GetUser(user.UserName);
            if (dbUser != null)
                throw new Exception("User with that username already exists.");
            using (SecurityUnitOfWork securityUnitOfWork = new SecurityUnitOfWork())
            {
                securityUnitOfWork.UserProfileRepository.Add(user);
                securityUnitOfWork.Complete();
            }
        }


        public static bool Login(string userName, string password, bool persistCookie = false)
        {
            return WebMatrix.WebData.WebSecurity.Login(userName, password, persistCookie);
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return WebMatrix.WebData.WebSecurity.ChangePassword(userName, oldPassword, newPassword);
        }

        public static string CreateUserAndAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebMatrix.WebData.WebSecurity.CreateUserAndAccount(userName, password, requireConfirmationToken);
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
