using Microsoft.AspNetCore.Mvc;
using MediatR;
using Chat.Core.Application.Commands.Messages;
using Chat.Core.Application.Queries.Messages;
using Chat.Core.Application.DTOs.Messages;
using LMSApp.Shared.DTOs;
using LMSApp.Shared.Extensions;


namespace Chat.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<MessageDto>>> SendMessage([FromBody] SendMessageCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return this.SuccessResponse(result, "Message sent successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequestResponse<MessageDto>(ex.Message);
            }
            
        }


        [HttpGet("conversation/{conversationId}")]
        public async Task<ActionResult<BaseListResponse<MessageDto>>> GetMessages(Guid conversationId)
        {
            
            try
            {
                var result = await _mediator.Send(new GetMessagesQuery(conversationId));

                if (result is null)
                {
                    return this.NotFoundListResponse<MessageDto>($"conversation not found");
                }

                return this.SuccessListResponse(result);

            }
            catch (Exception ex)
            {
                return this.BadRequestListResponse<MessageDto>(ex.Message);
            }

        }
        
    }
}
