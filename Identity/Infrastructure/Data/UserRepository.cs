using Identity.Core.Domain.Entities;
using Identity.Core.Domain.Interfaces;

namespace Identity.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        //private readonly IdentityDbContext _db = new();

        //public Task<User?> GetByEmailAsync(string email) =>
        //    Task.FromResult(_db.Users.FirstOrDefault(u => u.Email == email));

        //public Task<User?> GetByIdAsync(Guid id) =>
        //    Task.FromResult(_db.Users.FirstOrDefault(u => u.Id == id));

        //public Task AddAsync(User user)
        //{
        //    _db.Users.Add(user);
        //    return Task.CompletedTask;
        //}

        private static readonly List<User> _users = new(); 

        public Task<User?> GetByEmailAsync(string email) =>
            Task.FromResult(_users.FirstOrDefault(u => u.Email == email));

        public Task<User?> GetByIdAsync(Guid id) =>
            Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public Task AddAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
