using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Security;
using SecurityModule.Repositories;
using WebMatrix.WebData;

namespace SecurityModule
{
    public class InitSecurityDb : DropCreateDatabaseIfModelChanges<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {

            WebSecurity.InitializeDatabaseConnection("PhotoManagerDB_3_layer",
               "Users", "UserId", "UserName", autoCreateTables: true);
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (membership.GetUser("Bob", false) == null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Discriminator"] = "PayedUser";
                membership.CreateUserAndAccount("Bob", "test", false, parameters);
            }
            if (membership.GetUser("Joe", false) == null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Discriminator"] = "FreeUser";
                membership.CreateUserAndAccount("Joe", "test", false, parameters);
            }
        }
    }
}