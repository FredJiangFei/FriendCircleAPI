using Microsoft.AspNetCore.Mvc;

namespace FriendCircleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => StatusCode(201, "Ok");
    }
}
