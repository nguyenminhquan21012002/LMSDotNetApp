using Identity.Core.Application.Commands;
using Identity.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand cmd)
        {
            var user = await _mediator.Send(cmd);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return result is null ? Unauthorized() : Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user is null ? NotFound() : Ok(user);
        }
    }
}
