using System.Linq;
using SecurityModule.Entities;

namespace SecurityModule.Repositories
{
    public class UserProfileRepository: BaseRepository<UserProfile>
    {
        private SecurityContext _context;

        public UserProfileRepository(SecurityContext context) : base(context)
        {
            _context = context;
        }
        public UserProfile GetUserByName(string userName)
        {
            var query = from user in _context.UserProfiles
                        where user.UserName.Equals(userName)
                        select user;
            return query.FirstOrDefault();
        }
    }
}
