using System.Data.Entity;
using System.Web.Security;
using SecurityModule.Repositories;
using WebMatrix.WebData;

namespace SecurityModule
{
    public class InitSecurityDb : DropCreateDatabaseAlways<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {

            WebSecurity.InitializeDatabaseConnection("PhotoManagerDB_3_layer",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (membership.GetUser("Bob", false) == null)
            {
                membership.CreateUserAndAccount("Bob", "test", false);
            }
            if (membership.GetUser("Joe", false) == null)
            {
                membership.CreateUserAndAccount("Joe", "test", false);
            }
        }
    }
}