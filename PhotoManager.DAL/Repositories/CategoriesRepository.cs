using System.Collections.Generic;
using System.Linq;
using PhotoManagerModels.Models;


namespace PhotoManager.DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private PhotoManagerDbContext _context;

        public CategoryRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Category> GetCategoriesByAlbum(int id)
        {
            var query = from category in _context.Categories
                        where category.Albums.Any(album => album.Id == id)
                        select category;
            return query.ToList();
        }
        public List<Category> GetCategoriesByPhoto(int id)
        {
            var query = from category in _context.Categories
                        where category.Photoes.Any(photo => photo.Id == id)
                        select category;
            return query.ToList();
        }
    }
}