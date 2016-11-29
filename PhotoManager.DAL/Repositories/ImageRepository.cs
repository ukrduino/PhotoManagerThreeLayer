using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Repositories
{
    public class ImageRepository : BaseRepository<Image>
    {
        private PhotoManagerDbContext _context;

        public ImageRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
