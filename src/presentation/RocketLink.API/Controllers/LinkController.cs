using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketLink.Application.Features.Links.Commands.CreateLink;
using RocketLink.Application.Features.Links.Commands.IncreaseClickCount;
using RocketLink.Application.Features.Links.Commands.RemoveLink;
using RocketLink.Application.Features.Links.Commands.UpdateLink;
using RocketLink.Application.Features.Links.Commands.UpdatePriority;
using RocketLink.Application.Features.Links.Queries.GetAllByUser;
using RocketLink.Application.Features.Users.Queries.GetById;
using RocketLink.Application.Features.Users.Queries.GetByUsername;
using RocketLink.Domain.Common;

namespace RocketLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLink([FromBody] CreateLinkCommand command)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var res = await _mediator.Send(new GetByIdQuery(new Guid(userIdClaim)));

            if(!res.IsSuccess) return BadRequest(res);

            command.UserId = res.Value.Id;

            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUsername([FromQuery]string username)
        {
            if (username == null) return BadRequest(Result.Failure(new Error("User.NotFound", "User not found!")));

            var res = await _mediator.Send(new GetByUsernameQuery(username));

            if (!res.IsSuccess) return BadRequest(Result.Failure(new Error("User.NotFound", "User not found!")));

            var result = await _mediator.Send(new GetAllLinkByUserQuery(res.Value.Id));

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetLinksByToken")]
        public async Task<IActionResult> GetAllByToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var res = await _mediator.Send(new GetByIdQuery(new Guid(userIdClaim)));

            if (!res.IsSuccess) return BadRequest(res);

            var result = await _mediator.Send(new GetAllLinkByUserQuery(res.Value.Id, false));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateLink([FromBody] UpdateLinkCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser([FromRoute]string id)
        {
            var result = await _mediator.Send(new RemoveLinkCommand(new Guid(id)));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("increase/{id}")]
        public async Task<IActionResult> IncreaseClickCount([FromRoute]string id)
        {
            var result = await _mediator.Send(new IncreaseCountCommand(new Guid(id)));

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("priority")]
        public async Task<IActionResult> UpdatePriority(UpdatePriorityCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
