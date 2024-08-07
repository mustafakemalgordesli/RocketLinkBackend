using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketLink.Application.Features.Users.Queries.CheckEmailInUse;
using RocketLink.Application.Features.Users.Queries.CheckUsernamelInUse;
using RocketLink.Application.Features.Users.Queries.GetById;

namespace RocketLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("GetUserByToken")]
        public async Task<IActionResult> GetUserByToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var result = await _mediator.Send(new GetByIdQuery(new Guid(userIdClaim)));

            return Ok(result);
        }

        [HttpGet("CheckEmailInUse")]
        public async Task<IActionResult> CheckEmailInUse([FromQuery] string Email)
        {
            var result = await _mediator.Send(new CheckEmailInUseQuery(Email));

            return Ok(result);
        }

        [HttpGet("CheckUsernameInUse")]
        public async Task<IActionResult> CheckUsernameInUse([FromQuery] string Username)
        {
            var result = await _mediator.Send(new CheckUsernameInUseQuery(Username));

            return Ok(result);
        }
    }
}
