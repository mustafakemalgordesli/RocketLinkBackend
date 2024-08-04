using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RocketLink.Application.Features.Auth.Login;
using RocketLink.Application.Features.Auth.Register;

namespace RocketLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _meditor = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            var res = await _meditor.Send(command);
            if(!res.IsSuccess) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            var res = await _meditor.Send(command);
            if (!res.IsSuccess) return BadRequest(res);
            return Ok(res);
        }
    }
}
