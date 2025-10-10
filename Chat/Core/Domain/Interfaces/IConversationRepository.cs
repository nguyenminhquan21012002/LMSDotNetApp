using Chat.Core.Domain.Entities;

namespace Chat.Core.Domain.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation> AddAsync(Conversation conversation);
        Task<Conversation?> GetByIdAsync(Guid id);
        Task<List<Conversation>> GetByUserIdAsync(Guid userId);
        Task<Conversation?> GetDirectConversationAsync(Guid user1Id, Guid user2Id);
        Task<Conversation> UpdateAsync(Conversation conversation);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}

