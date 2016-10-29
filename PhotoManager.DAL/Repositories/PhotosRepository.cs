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
    }
}