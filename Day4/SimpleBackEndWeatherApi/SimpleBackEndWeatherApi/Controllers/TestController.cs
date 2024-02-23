using Microsoft.AspNetCore.Mvc;

namespace SimpleBackEndWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(new { Message = "Test success from api", Time = DateTime.UtcNow.ToLongTimeString() });
        }
    }
}
