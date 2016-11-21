using System.Data.Entity;
using SecurityModule.Models;

namespace SecurityModule.Repositories
{
    public class SecurityContext : DbContext
    {
        public SecurityContext()
            : base("PhotoManagerDB_3_layer")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
