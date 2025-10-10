using Chat.Core.Domain.Enums;
using Chat.Core.Domain.Entities;
using Chat.Core.Application.DTOs.Messages;

namespace Chat.Core.Application.DTOs.Conversations
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public ConversationType Type { get; set; }
        public ICollection<UserDto> Participants { get; set; } = new List<UserDto>();
        public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
        public MessageDto? LastMessage { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
