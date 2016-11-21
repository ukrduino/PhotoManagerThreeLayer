using System;
using System.Configuration;

namespace PhotoManager.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly PhotoManagerDbContext _context;
        public PhotoRepository Photos { get; }
        public AlbumRepository Albums { get; }
        public PhotoCommentsRepository PhotoComments { get; }
        public UsersRepository Users { get; }

        public UnitOfWork(PhotoManagerDbContext context)
        {
            _context = context;
            Photos = new PhotoRepository(_context);
            Albums = new AlbumRepository(_context);
            PhotoComments = new PhotoCommentsRepository(_context);
            Users = new UsersRepository(_context);
        }


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