using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimiter.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RateLimiterTestController : ControllerBase
    {

        [HttpGet("unlimited")]
        public ActionResult<string> GetUnlimited()
        {
            return Ok("Unlimited! Let's Go!");
        }

        [HttpGet("limited")]
        public ActionResult<string> GetLimited()
        {
            return Ok("Limited, don't over use me!");
        }

    }
}
