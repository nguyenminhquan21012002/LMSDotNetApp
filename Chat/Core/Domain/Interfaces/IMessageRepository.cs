using Chat.Core.Domain.Entities;

namespace Chat.Core.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddAsync(Message message);
        Task<Message?> GetByIdAsync(Guid id);
        Task<List<Message>> GetByConversationIdAsync(Guid conversationId);
        Task<Message> UpdateAsync(Message message);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
