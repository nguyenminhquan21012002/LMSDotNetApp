using MediatR;

namespace Chat.Core.Application.Commands.Conversations
{
    public record DeleteConversationCommand(Guid Id) : IRequest<bool>
    {
    }
}

