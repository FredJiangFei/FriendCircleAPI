using System.Linq;
using System.Threading.Tasks;
using FriendCircle.Data;
using FriendCircle.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace FriendCircle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MySqlDbContext _context;
        public UsersController(MySqlDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User request)
        {
            _context.Users.Add(request);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{userId}/relations")]
        public async Task<IActionResult> AddFriend(string userId, FriendRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                user.Friends.Add(new Relation
                {
                    UserId = userId,
                    FriendId = request.FriendId
                });
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
