using Chat.Core.Application.Commands.Conversations;
using Chat.Core.Domain.Interfaces;
using MediatR;

namespace Chat.Core.Application.Handlers.Conversations
{
    public class DeleteConversationHandler : IRequestHandler<DeleteConversationCommand, bool>
    {
        private readonly IConversationRepository _conversationRepository;

        public DeleteConversationHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<bool> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.Id);
            if (conversation == null)
            {
                return false;
            }

            await _conversationRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
