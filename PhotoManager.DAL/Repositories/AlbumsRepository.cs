using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhotoManager.DAL.Models;


namespace PhotoManager.DAL.Repositories
{
    public class AlbumRepository : BaseRepository<Album>
    {
        private PhotoManagerDbContext _context;

        public AlbumRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Album> GetAlbumsByPhoto(int id)
        {
            var query = from album in _context.Albums
                        where album.Photos.Any(photo => photo.Id == id)
                        select album;
            return query.ToList();
        }
        public Album GetAlbumById(int id)
        {
            //TODO Do we need to include image
            return _context.Albums.Include(user => user.User).Include(image=>image.Image).FirstOrDefault(alb => alb.Id == id);
        }

        public List<Album> GetAlbumsByUser(User user)
        {
            var query = from albums in _context.Albums
                        where albums.User.UserId == user.UserId
                        select albums;
            return query.ToList();
        }

        public void UpdateAlbum(Album album)
        {
            _context.Albums.Attach(album);
            var albumFromDb = _context.Entry(album);
            albumFromDb.State = EntityState.Modified;

            albumFromDb.Property(alb => alb.CreatedDate).IsModified = false;
            if (album.Image == null)
            {
                albumFromDb.Property(alb => alb.Image).IsModified = false;
            }
        }

        public void RemovePhotosFromAlbum(int albumId, List<int> photoIds)
        {
            Album album = _context.Albums.Include("Photos").SingleOrDefault(alb=>alb.Id == albumId);
            List<Photo> photoesToRemove = _context.Photos.Where(photo => photoIds.Contains(photo.Id)).ToList();
            photoesToRemove.ForEach(photo=> album.Photos.Remove(photo));
        }
    }
}