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

        public async Task<Quizzes> CreateAsync(Quizzes quiz)
        {
            var document = _mapper.Map<QuizDocument>(quiz);
            await _collection.InsertOneAsync(document);
            return _mapper.Map<Quizzes>(document);
        }

        public async Task<Quizzes?> UpdateAsync(Quizzes quiz)
        {
            var filter = Builders<QuizDocument>.Filter.Eq(x => x.Id, quiz.Id);
            var update = Builders<QuizDocument>.Update
                .Set(x => x.Title, quiz.Title)
                .Set(x => x.Description, quiz.Description)
                .Set(x => x.TimeLimit, quiz.TimeLimit)
                .Set(x => x.TotalPoints, quiz.TotalPoints)
                .Set(x => x.Type, (int)quiz.Type);

            var updateResult = await _collection.UpdateOneAsync(filter, update);
            return quiz;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<QuizDocument>.Filter.Eq(x => x.Id, id);
            var task = await _collection.DeleteOneAsync(filter);
            return task.DeletedCount > 0;
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
