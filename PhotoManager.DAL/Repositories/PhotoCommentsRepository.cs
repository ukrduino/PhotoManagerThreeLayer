using System.Collections.Generic;
using System.Linq;
using PhotoManager.DAL.Models;


namespace PhotoManager.DAL.Repositories
{
    public class PhotoCommentsRepository : BaseRepository<PhotoComment>
    {
        private PhotoManagerDbContext _context;

        public PhotoCommentsRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }
        public List<PhotoComment> GetCommentsByPhoto(int id)
        {
            var query = from comment in _context.PhotoComments
                        where comment.PhotoId == id
                        select comment;
            return query.ToList();
        }
    }
}