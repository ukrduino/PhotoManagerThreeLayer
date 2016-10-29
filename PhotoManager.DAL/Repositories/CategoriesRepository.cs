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
    }
}