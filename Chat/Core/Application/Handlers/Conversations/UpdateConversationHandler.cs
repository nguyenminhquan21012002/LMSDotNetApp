using Chat.Core.Application.Commands.Conversations;
using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers
{
    public class UpdateConversationHandler : IRequestHandler<UpdateConversationCommand, ConversationDto>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public UpdateConversationHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ConversationDto> Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.Id);
            if (conversation == null)
            {
                throw new ArgumentException($"Conversation with ID {request.Id} not found.");
            }

            // Update properties if provided
            if (!string.IsNullOrEmpty(request.Name))
            {
                // Note: Conversation entity doesn't have Name property yet
                // You might need to add it to the entity if needed
            }

            var updatedConversation = await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<ConversationDto>(updatedConversation);
        }
    }
}
