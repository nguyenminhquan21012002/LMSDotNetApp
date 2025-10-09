using Chat.Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace Chat.Core.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        [JsonIgnore]
        public ICollection<UserConversation> UserConversations { get; set; } = new List<UserConversation>();
    }
}
