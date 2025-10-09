using Chat.Core.Application.DTOs.Conversations;
using MediatR;

namespace Chat.Core.Application.Commands.Conversations
{
    public record UpdateConversationCommand(Guid Id, string? Name) : IRequest<ConversationDto>
    {
    }
}

