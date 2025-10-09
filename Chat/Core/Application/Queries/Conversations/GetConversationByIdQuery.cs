using Chat.Core.Application.DTOs.Conversations;
using MediatR;

namespace Chat.Core.Application.Queries.Conversations
{
    public record GetConversationByIdQuery(Guid Id) : IRequest<ConversationDto?>
    {
    }
}
