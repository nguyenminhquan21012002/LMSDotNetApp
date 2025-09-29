namespace Identity.Core.Domain.Events
{
    public class UserRegisteredEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
