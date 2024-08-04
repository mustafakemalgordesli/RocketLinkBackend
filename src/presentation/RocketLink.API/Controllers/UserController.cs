using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUserByToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var result = _mediator.Send(new GetByIdQuery(new Guid(userIdClaim)));

            return Ok(result);
        }
    }
}
