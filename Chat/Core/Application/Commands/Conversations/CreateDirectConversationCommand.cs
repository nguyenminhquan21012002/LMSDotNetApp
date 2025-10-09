using Chat.Core.Application.DTOs.Conversations;
using MediatR;

namespace Chat.Core.Application.Commands.Conversations
{
    public record CreateDirectConversationCommand(Guid User1Id, Guid User2Id) : IRequest<ConversationDto>
    {
    }
}
