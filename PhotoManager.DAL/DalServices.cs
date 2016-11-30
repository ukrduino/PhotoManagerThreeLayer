using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Security;
using WebMatrix.WebData;

namespace PhotoManager.DAL
{
    public class DalServices
    {
        public void DalSetUpDb()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoManagerDbContext>());
            PhotoManagerDbContext context = new PhotoManagerDbContext();
            context.Database.Initialize(true);
            if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("PhotoManagerDB_3_layer", "Users", "UserId", "UserName", autoCreateTables: true);
        }
    }
}

