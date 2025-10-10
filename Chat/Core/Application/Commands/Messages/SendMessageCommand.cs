using Chat.Core.Application.DTOs.Messages;
using MediatR;

namespace Chat.Core.Application.Commands.Messages
{
    public record SendMessageCommand(Guid ConversationId, Guid SenderId, string Content) : IRequest<MessageDto>
    {
    }
}
