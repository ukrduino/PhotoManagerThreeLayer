using System.Collections.Generic;
using System.Linq;
using PhotoManagerModels.Models;

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
        public List<Album> GetAlbumsByCategory(int id)
        {
            var query = from album in _context.Albums
                        where album.Categories.Any(cat => cat.Id == id)
                        select album;
            return query.ToList();
        }
    }
}