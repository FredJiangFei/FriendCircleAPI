using System.Threading.Tasks;
using FriendCircle.Data;
using FriendCircle.Repositories.Interfaces;

namespace FriendCircle.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlDbContext _context;
        public UserRepository(MySqlDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}