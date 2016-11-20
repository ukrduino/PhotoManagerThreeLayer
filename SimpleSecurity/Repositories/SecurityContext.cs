using System.Data.Entity;
using SecurityModule.Entities;

namespace SecurityModule.Repositories
{
    public class SecurityContext : DbContext
    {
        public SecurityContext()
            : base("PhotoManagerDB_3_layer")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
