using System.Linq;
using System.Threading.Tasks;
using FriendCircle.Data;
using FriendCircle.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            var user = _context.Users.Single(x => x.UserId == userId);
            var response = new UserResponse
            {
                Data = new UserResponseData
                {
                    UserId = userId,
                    Name = user.Name
                }
            };
            return Ok(response);
        }

        [HttpPost("{userId}/relations")]
        public async Task<IActionResult> AddFriend(string userId, FriendRequest request)
        {
            var user = _context.Users.Single(x => x.UserId == userId);
            user.Friends.Add(new Relation
            {
                UserId = userId,
                FriendId = request.FriendId
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userId}/relations/{friendId}")]
        public async Task<IActionResult> DeleteFriend(string userId, string friendId)
        {
            var user = _context.Users.Include(x => x.Friends).Single(x => x.UserId == userId);
            var friend = user.Friends.Single(x => x.FriendId == friendId);
            user.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{userId}/moments")]
        public async Task<IActionResult> AddMoment(string userId, MomentRequest request)
        {
            var user = _context.Users.Single(x => x.UserId == userId);
            user.Moments.Add(new Moment
            {
                UserId = userId,
                MomentId = request.MomentId,
                Content = request.Content
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userId}/moments/{momentId}")]
        public async Task<IActionResult> DeleteMoment(string userId, string momentId)
        {
            var user = _context.Users.Include(x => x.Moments).Single(x => x.UserId == userId);
            var moment = user.Moments.Single(x => x.MomentId == momentId);
            user.Moments.Remove(moment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{userId}/streams")]
        public IActionResult GetMoments(string userId, string lastId)
        {


            return Ok();
        }
    }
}
