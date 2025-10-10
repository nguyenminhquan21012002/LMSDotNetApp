using Chat.Core.Application.Commands.Messages;
using Chat.Core.Application.DTOs.Messages;
using Chat.Core.Domain.Entities;
using Chat.Core.Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Chat.Core.Application.Handlers.Messages
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public SendMessageHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ConversationId = request.ConversationId,
                SenderId = request.SenderId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);

            return _mapper.Map<MessageDto>(message);
        }
    }
}
