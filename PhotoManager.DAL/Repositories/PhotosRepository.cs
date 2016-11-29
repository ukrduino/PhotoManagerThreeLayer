using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhotoManager.DAL.Models;


namespace PhotoManager.DAL.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private PhotoManagerDbContext _context;

        public PhotoRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Photo> GetPhotosByAlbum(int id)
        {
            var query = from photo in _context.Photos
                where photo.Albums.Any(album => album.Id == id)
                select photo;
            return query.ToList();
        }

        public List<Photo> GetPhotosByUser(User user)
        {
            var query = from photos in _context.Photos
                where photos.UserId == user.UserId
                select photos;
            return query.ToList();
        }
        public Photo GetPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(photo => photo.Id == id);
        }
    }
}