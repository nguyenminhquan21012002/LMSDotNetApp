using Chat.Core.Application.Queries.Conversations;
using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers.Conversations
{
    public class GetConversationByIdHandler : IRequestHandler<GetConversationByIdQuery, ConversationDto?>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public GetConversationByIdHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ConversationDto?> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.Id);
            return conversation != null ? _mapper.Map<ConversationDto>(conversation) : null;
        }
    }
}
