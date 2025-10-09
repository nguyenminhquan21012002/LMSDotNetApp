using Chat.Core.Application.Queries.Conversations;
using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Domain.Entities;
using Chat.Core.Domain.Interfaces;
using Chat.Core.Domain.Enums;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers.Conversations
{
    public class GetOrCreateDirectConversationHandler : IRequestHandler<GetOrCreateDirectConversationQuery, ConversationDto>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public GetOrCreateDirectConversationHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ConversationDto> Handle(GetOrCreateDirectConversationQuery request, CancellationToken cancellationToken)
        {
            // Tìm conversation hiện tại
            var existingConversation = await _conversationRepository.GetDirectConversationAsync(request.User1Id, request.User2Id);
            
            if (existingConversation != null)
            {
                return _mapper.Map<ConversationDto>(existingConversation);
            }

            // Tạo conversation mới nếu chưa có
            var conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                Type = ConversationType.Direct
            };

            var userConversations = new List<UserConversation>
            {
                new UserConversation
                {
                    UserId = request.User1Id,
                    ConversationId = conversation.Id,
                    CreateAt = DateTime.UtcNow
                },
                new UserConversation
                {
                    UserId = request.User2Id,
                    ConversationId = conversation.Id,
                    CreateAt = DateTime.UtcNow
                }
            };

            conversation.UserConversations = userConversations;

            await _conversationRepository.AddAsync(conversation);
            return _mapper.Map<ConversationDto>(conversation);
        }
    }
}
