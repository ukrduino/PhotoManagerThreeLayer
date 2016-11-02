using System.Collections.Generic;
using System.Linq;
using PhotoManagerModels.Models;


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
            var query = from photo in _context.Photoes
                         where photo.Albums.Any(album => album.Id == id)
                        select photo;
            return query.ToList();
        }
        public List<Photo> GetPhotosByCategory(int id)
        {
            var query = from photo in _context.Photoes
                        where photo.Categories.Any(cat => cat.Id == id)
                        select photo;
            return query.ToList();
        }
    }
}