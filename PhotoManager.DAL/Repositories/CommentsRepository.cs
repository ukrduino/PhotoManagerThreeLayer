using PhotoManagerModels.Models;


namespace PhotoManager.DAL.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private PhotoManagerDbContext _context;

        public CommentRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}