using System.Linq;
using System.Threading.Tasks;
using FriendCircleAPI.Data;
using FriendCircleAPI.RequestModels;
using FriendCircleAPI.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FriendCircleAPI.Controllers
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
            var user = _context.Users
                .Include(x => x.Friends)
                .Include(x => x.FriendOf)
                .Single(x => x.UserId == userId);

            var friendIds = user.Friends.Select(x => x.FriendId).ToList();
            var friendOfIds = user.FriendOf.Select(x => x.UserId).ToList();
            var momentUserIds = friendIds.Intersect(friendOfIds).Append(userId);

            var moments = _context.Moments
            .Include(x => x.User)
            .Where(x => momentUserIds.Contains(x.User.UserId));

            if (!string.IsNullOrEmpty(lastId))
            {
                var lastMoment = moments.FirstOrDefault(x => x.MomentId == lastId);
                if (lastMoment != null)
                {
                    moments = moments.Where(x => x.Created < lastMoment.Created);
                }
            }

            var momentResult = moments
            .OrderByDescending(x => x.Created)
            .Take(50);

            var momentResponse = new MomentResponse
            {
                Data = momentResult.Select(x => new MomentResponseData
                {
                    MomentId = x.MomentId,
                    MomentUserId = x.User.UserId,
                    Content = x.Content
                }).ToArray()
            };

            return Ok(momentResponse);
        }
    }
}
