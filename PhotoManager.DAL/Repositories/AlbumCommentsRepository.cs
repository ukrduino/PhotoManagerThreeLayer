using System.Collections.Generic;
using System.Linq;
using PhotoManagerModels.Models;


namespace PhotoManager.DAL.Repositories
{
    public class AlbumCommentsRepository : BaseRepository<AlbumComment>
    {
        private PhotoManagerDbContext _context;

        public AlbumCommentsRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public List<AlbumComment> GetCommentsByAlbum(int id)
        {
            var query = from comment in _context.AlbumComments
                        where comment.AlbumID == id
                        select comment;
            return query.ToList();
        }
    }
}