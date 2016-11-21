using System;
using System.Data.Entity;
using SecurityModule.Models;
using SecurityModule.Repositories;

namespace SecurityModule
{
    public static class WebSecurityService
    {
        public static void InitSecurityDataBase()
        {
            Database.SetInitializer(new InitSecurityDb());
            SecurityContext context = new SecurityContext();
            context.Database.Initialize(true);
            if (!WebMatrix.WebData.WebSecurity.Initialized)
                WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("PhotoManagerDB_3_layer",
                    "Users", "UserId", "UserName", autoCreateTables: true);
        }


        public static User GetUser(string username)
        {
            using (SecurityUnitOfWork securityUnitOfWork = new SecurityUnitOfWork())
            {
                return securityUnitOfWork.UsersRepository.GetUserByName(username);
            }
        }

        public static User GetCurrentUser()
        {
            return GetUser(CurrentUserName);
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
