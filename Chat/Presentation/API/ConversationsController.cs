using Microsoft.AspNetCore.Mvc;
using MediatR;
using Chat.Core.Application.Commands.Conversations;
using Chat.Core.Application.Queries.Conversations;
using Chat.Core.Application.DTOs.Conversations;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Extensions;

namespace Chat.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("direct")]
        public async Task<ActionResult<BaseResponse<ConversationDto>>> CreateDirectConversation([FromBody] CreateDirectConversationCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return this.SuccessResponse(result);
            }
            catch(Exception ex)
            {
                return this.BadRequestResponse<ConversationDto>(ex.Message);

            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ConversationDto>>> GetConversation(Guid id)
        {
            try
            {
                var query = new GetConversationByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result is null)
                {
                    return this.NotFoundResponse<ConversationDto>($"conversation with ID {id} not found");
                }

                return this.SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.BadRequestResponse<ConversationDto>(ex.Message);
            }
        }

      
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<BaseListResponse<ConversationDto>>> GetUserConversations(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new GetUserConversationsQuery(userId));

                if (result is null)
                {
                    return this.NotFoundListResponse<ConversationDto>($"conversation not found");
                }

                return this.SuccessListResponse(result);
               
            }
            catch (Exception ex)
            {
                return this.BadRequestListResponse<ConversationDto>(ex.Message);
            }
            
        }


        [HttpGet("direct/{user1Id}/{user2Id}")]
        public async Task<ActionResult<BaseResponse<ConversationDto>>> GetOrCreateDirectConversation([FromBody] GetOrCreateDirectConversationQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return this.SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.BadRequestResponse<ConversationDto>(ex.Message);

            }
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateConversation(Guid id, [FromBody] UpdateConversationDto dto)
        //{
        //    var command = new UpdateConversationCommand(id, dto.Name);
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}


        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> DeleteConversation(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteConversationCommand(id));
                return this.SuccessResponse(result, "Conversation deleted successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequestResponse<bool>(ex.Message);
            }
        }

    }
}
