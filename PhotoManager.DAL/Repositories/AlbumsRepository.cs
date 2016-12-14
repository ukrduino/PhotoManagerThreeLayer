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
            return _context.Albums.FirstOrDefault(alb => alb.Id == id);
        }

        public List<Album> GetAlbumsByUser(User user)
        {
            var query = from albums in _context.Albums
                        where albums.UserId == user.UserId
                        select albums;
            return query.ToList();
        }

        public void UpdateAlbum(Album album)
        {
            _context.Albums.Attach(album);
            var albumFromDb = _context.Entry(album);
            albumFromDb.State = EntityState.Modified;
            albumFromDb.Property(alb => alb.CreatedDate).IsModified = false;
        }

        public void RemovePhotoFromAlbum(int albumId, int photoId)
        {
            Album album = _context.Albums.Include("Photos").SingleOrDefault(alb=>alb.Id == albumId);
            Photo photo = _context.Photos.SingleOrDefault(pht => pht.Id.Equals(photoId));
            if (album != null && album.Photos.Contains(photo))
            {
                album.Photos.Remove(photo);
            }
        }

        public void AddPhotoToAlbum(int albumId, int photoId)
        {
            Album album = _context.Albums.Include("Photos").SingleOrDefault(alb=>alb.Id == albumId);
            Photo photo = _context.Photos.SingleOrDefault(pht => pht.Id.Equals(photoId));
            if (album != null && !album.Photos.Contains(photo))
            {
                album.Photos.Add(photo);
            }
        }
    }
}