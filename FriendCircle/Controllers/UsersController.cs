using System.Threading.Tasks;
using FriendCircle.Data;
using FriendCircle.Repositories.Interfaces;
using FriendCircle.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace FriendCircle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = new User
            {
                UserId = request.UserId,
                Name = request.Name
            };
            await _userRepository.Add(user);
            return Ok(request.UserId);
        }
    }
}
