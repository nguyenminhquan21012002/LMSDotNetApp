using Identity.Core.Application.Commands;
using Identity.Core.Application.DTOs;
using Identity.Core.Application.Queries;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Extensions;
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
        public async Task<ActionResult<BaseResponse<UserDto>>> Register([FromBody] RegisterUserCommand cmd)
        {
            try
            {
                var user = await _mediator.Send(cmd);
                return this.SuccessResponse(user, "User registered successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequestResponse<UserDto>(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<AuthResultDto>>> Login([FromBody] LoginUserCommand cmd)
        {
            try
            {
                var result = await _mediator.Send(cmd);

                if (result is null)
                    return this.ValidationErrorResponse<AuthResultDto>("Invalid credentials");

                return this.SuccessResponse(result, "Login successful");
            }
            catch (Exception ex)
            {
                return this.InternalServerErrorResponse<AuthResultDto>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<UserDto>>> GetUser(Guid id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));

                if (user is null)
                    return this.NotFoundResponse<UserDto>($"User with ID {id} not found");

                return this.SuccessResponse(user, "User retrieved successfully");
            }
            catch (Exception ex)
            {
                return this.InternalServerErrorResponse<UserDto>(ex.Message);
            }
        }
    }
}
