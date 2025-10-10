using AutoMapper;
using MongoDB.Driver;
using Quiz.Core.Domain.Entities;
using Quiz.Core.Domain.Interfaces;
using Quiz.Infrastructure.Data.DbContexts;
using Quiz.Infrastructure.Data.MongoDbDocuments;

namespace Quiz.Infrastructure.Data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IMongoCollection<QuizDocument> _collection;
        private readonly IMapper _mapper;
        private readonly ILogger<QuizRepository> _logger;

        public QuizRepository(MongoDbContext context, IMapper mapper, ILogger<QuizRepository> logger)
        {
            _collection = context.Database?.GetCollection<QuizDocument>("quiz") 
                ?? throw new ArgumentNullException(nameof(context), "Database connection is not available");
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Quizzes?> GetByIdAsync(string id)
        {
            var filter = Builders<QuizDocument>.Filter.Eq(x => x.Id, id);
            var task = await _collection.FindAsync(filter);
            var document = await task.FirstOrDefaultAsync();
            return _mapper.Map<Quizzes>(document);
        }
    }
}
