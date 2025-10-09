using Chat.Core.Domain.Entities;

namespace Chat.Core.Application.DTOs.Messages
{
    public class SendMessageDto
    {
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
