using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketLink.Application.Features.Users.Commands.RemoveUser;
using RocketLink.Application.Features.Users.Commands.UpdateUser;
using RocketLink.Application.Features.Users.Commands.UploadImage;
using RocketLink.Application.Features.Users.Queries.CheckEmailInUse;
using RocketLink.Application.Features.Users.Queries.CheckUsernamelInUse;
using RocketLink.Application.Features.Users.Queries.GetById;
using RocketLink.Application.Features.Users.Queries.GetByUsername;
using RocketLink.Domain.Common;

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

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var result = await _mediator.Send(new RemoveUserCommand(new Guid(userIdClaim)));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            var query = new GetByUsernameQuery(username);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("CheckEmailInUse")]
        public async Task<IActionResult> CheckEmailInUse([FromQuery] string Email)
        {
            var result = await _mediator.Send(new CheckEmailInUseQuery(Email));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("CheckUsernameInUse")]
        public async Task<IActionResult> CheckUsernameInUse([FromQuery] string Username)
        {
            var result = await _mediator.Send(new CheckUsernameInUseQuery(Username));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm]IFormFile file)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var result = await _mediator.Send(new UploadImageCommand(new Guid(userIdClaim), file));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserCommand command)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            command.Id = new Guid(userIdClaim);

            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
