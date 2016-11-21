using System;

namespace SecurityModule.Repositories
{
    public class SecurityUnitOfWork  : IDisposable
    {
        private SecurityContext _context = new SecurityContext();

        public SecurityUnitOfWork()
        {
            UsersRepository = new UsersRepository(_context);
        }

        public UsersRepository UsersRepository { get; }

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
