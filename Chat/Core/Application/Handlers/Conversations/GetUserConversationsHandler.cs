using Chat.Core.Application.Queries.Conversations;
using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers.Conversations
{
    public class GetUserConversationsHandler : IRequestHandler<GetUserConversationsQuery, List<ConversationDto>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public GetUserConversationsHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<List<ConversationDto>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _conversationRepository.GetByUserIdAsync(request.UserId);
            return _mapper.Map<List<ConversationDto>>(conversations);
        }
    }
}
