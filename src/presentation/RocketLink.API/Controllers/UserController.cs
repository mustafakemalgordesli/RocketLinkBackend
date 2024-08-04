using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RocketLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet("GetUserByToken")]
        public IActionResult GetUserByToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            return Ok(userIdClaim);
        }
    }
}
