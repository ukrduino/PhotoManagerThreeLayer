using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using PhotoManagerModels.Models;
using PhotoManagerModels.Models.Interfaces;


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
                    IHasLastModifiedField lastModified = entry.Entity as IHasLastModifiedField;
                    if (lastModified != null)
                        lastModified.LastModified = now;
                }
            }

            return base.SaveChanges();
        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Photo> Photoes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<PhotoComment> PhotoComments { get; set; }
        public virtual DbSet<AlbumComment> AlbumComments { get; set; }
    }
}