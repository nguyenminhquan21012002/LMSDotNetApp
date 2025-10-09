namespace Chat.Core.Domain.Entities
{
    public class UserConversation
    {
        public Guid UserId { get; set; }
        public Guid ConversationId { get; set; }
        public User User { get; set; } = null!;
        public Conversation Conversation { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    }
}
