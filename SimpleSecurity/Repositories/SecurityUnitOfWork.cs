using System;

namespace SecurityModule.Repositories
{
    public class SecurityUnitOfWork  : IDisposable
    {
        private SecurityContext _context = new SecurityContext();

        public SecurityUnitOfWork()
        {
            UserProfileRepository = new UserProfileRepository(_context);
        }

        public UserProfileRepository UserProfileRepository { get; }

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
