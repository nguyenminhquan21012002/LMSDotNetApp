using Chat.Core.Application.Queries.Messages;
using Chat.Core.Application.DTOs.Messages;
using Chat.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers.Messages
{
    public class GetMessageHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessageHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetByConversationIdAsync(request.ConversationId);

            return _mapper.Map<List<MessageDto>>(messages);
        }
    }
}
