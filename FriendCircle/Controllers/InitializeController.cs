using FriendCircle.Data;
using Microsoft.AspNetCore.Mvc;

namespace FriendCircle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitializeController : ControllerBase
    {
        private readonly MySqlDbContext _context;
        public InitializeController(MySqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DbInitializer.Initialize(_context);
            return Ok("Ok");
        }
    }
}
