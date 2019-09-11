using Microsoft.AspNetCore.Mvc;

namespace FriendCircle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => StatusCode(201, "Ok");
    }
}
