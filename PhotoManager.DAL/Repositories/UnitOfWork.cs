using System;

namespace PhotoManager.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly PhotoManagerDbContext _context;

        public UnitOfWork(PhotoManagerDbContext context)
        {
            _context = context;
            Photos = new PhotoRepository(_context);
            Albums = new AlbumRepository(_context);
            Categories = new CategoryRepository(_context);
            Comments = new CommentRepository(_context);
        }

        public PhotoRepository Photos { get; private set; }
        public AlbumRepository Albums { get; private set; }
        public CategoryRepository Categories { get; private set; }
        public CommentRepository Comments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}