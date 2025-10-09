using Chat.Core.Application.DTOs.Conversations;
using Chat.Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace Chat.Core.Application.DTOs.Messages
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        [JsonIgnore]
        public ConversationDto ConversationDto { get; set; } = null!;

        public Guid SenderId { get; set; }
        [JsonIgnore]

        public UserDto SenderDto { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
