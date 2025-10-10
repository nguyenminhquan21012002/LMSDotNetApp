using Chat.Core.Domain.Entities;
using Chat.Core.Domain.Enums;
using Chat.Core.Domain.Interfaces;
using Chat.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ChatDbContext _context;

        public ConversationRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> AddAsync(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task<Conversation?> GetByIdAsync(Guid id)
        {
            return await _context.Conversations
                .Include(c => c.UserConversations)
                    .ThenInclude(uc => uc.User)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Conversation>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Conversations
                .Include(c => c.UserConversations)
                    .ThenInclude(uc => uc.User)
                .Include(c => c.Messages.OrderByDescending(m => m.SentAt).Take(1))
                .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                .OrderByDescending(c => c.UserConversations
                    .SelectMany(uc => uc.Conversation.Messages)
                    .OrderByDescending(m => m.SentAt)
                    .FirstOrDefault() != null ? 
                    c.UserConversations
                        .SelectMany(uc => uc.Conversation.Messages)
                        .OrderByDescending(m => m.SentAt)
                        .First().SentAt : 
                    DateTime.MinValue)
                .ToListAsync();
        }

        public async Task<Conversation?> GetDirectConversationAsync(Guid user1Id, Guid user2Id)
        {
            return await _context.Conversations
                .Include(c => c.UserConversations)
                    .ThenInclude(uc => uc.User)
                .Include(c => c.Messages)
                .Where(c => c.Type == ConversationType.Direct)
                .Where(c => c.UserConversations.Count() == 2)
                .Where(c => c.UserConversations.Any(uc => uc.UserId == user1Id) &&
                           c.UserConversations.Any(uc => uc.UserId == user2Id))
                .FirstOrDefaultAsync();
        }

        public async Task<Conversation> UpdateAsync(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task DeleteAsync(Guid id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Conversations.AnyAsync(c => c.Id == id);
        }
    }
}

