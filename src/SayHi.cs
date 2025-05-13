using Microsoft.AspNetCore.Mvc;

namespace MyNamespace.Controllers
{
    [ApiController]
    [Route("hi")]
    public class SayHi : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello! Thanks for the GET request!");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Thanks for the post!");
        }
    }
}
