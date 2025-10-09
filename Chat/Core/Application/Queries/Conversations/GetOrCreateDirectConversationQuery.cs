using Chat.Core.Application.DTOs.Conversations;
using MediatR;

namespace Chat.Core.Application.Queries.Conversations
{
    public record GetOrCreateDirectConversationQuery(Guid User1Id, Guid User2Id) : IRequest<ConversationDto>
    {
    }
}
