using Chat.Core.Application.DTOs.Messages;
using MediatR;

namespace Chat.Core.Application.Queries.Messages
{
    public record GetMessagesQuery(Guid ConversationId) : IRequest<List<MessageDto>>
    {
    }
}
