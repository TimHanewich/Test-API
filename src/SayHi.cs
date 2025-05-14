using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyNamespace.Controllers
{
    [ApiController]
    [Route("hi")]
    public class SayHi : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            Response.StatusCode = 200;
            Response.Headers.Append("Content-Type", "application/json");
            await Response.WriteAsync("{\"name\": \"tim\"}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Thanks for the post!");
        }
    }
}
