using System.Data.Entity;

namespace PhotoManager.DAL
{
    public class DalServices
    {
        public void DalSetUpDb()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoManagerDbContext>());
        }
    }
}
