using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Models.Interfaces;


namespace PhotoManager.DAL
{
    public class PhotoManagerDbContext : DbContext
    {

        public PhotoManagerDbContext() : base("name=PhotoManagerDB_3_layer")
        {
        }

        public override int SaveChanges()
        {
            DateTime now = DateTime.Now;
            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (!entry.IsRelationship)
                {
                    IHasModifiedField modified = entry.Entity as IHasModifiedField;
                    if (modified != null)
                        modified.Modified = now;
                }
            }

            return base.SaveChanges();
        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<PhotoComment> PhotoComments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public List<Photo> SearchPhotos(string searchText, int userId)
        {
            SqlParameter param1 = new SqlParameter("@SearchText", searchText);
            SqlParameter param2 = new SqlParameter("@UserId", userId);
            return Database.SqlQuery<Photo>("SearchPhotos @SearchText, @UserId", param1, param2).ToList();
        }
    }
}