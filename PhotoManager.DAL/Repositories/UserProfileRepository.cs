using System.Linq;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Repositories
{
    public class UsersRepository: BaseRepository<User>
    {
        private PhotoManagerDbContext _context;

        public UsersRepository(PhotoManagerDbContext context) : base(context)
        {
            _context = context;
        }
        public User GetUserByName(string userName)
        {
            var query = from dbuser in _context.Users
                        where dbuser.UserName.Equals(userName)
                        select dbuser;
            var user = query.FirstOrDefault();
            if (user is PayedUser){
                return user as PayedUser;
            }
            return user as FreeUser;
        }
        public User GetUserById(int userId)
        {
            var query = from dbuser in _context.Users
                        where dbuser.UserId.Equals(userId)
                        select dbuser;
            var user = query.FirstOrDefault();
            if (user is PayedUser)
            {
                return user as PayedUser;
            }
            return user as FreeUser;
        }
    }
}
