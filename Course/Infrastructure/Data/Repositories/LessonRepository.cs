using AutoMapper;
using Course.Core.Domain.Entities;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Course.Infrastructure.Data.MongoDbDocuments;
using MongoDB.Driver;

namespace Course.Infrastructure.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IMongoCollection<LessonDocument> _document;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonRepository> _logger;

        public LessonRepository(MongoDbContext context, IMapper mapper, ILogger<LessonRepository> logger)
        {
            _document = context.Database?.GetCollection<LessonDocument>("lesson");
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            var task = await _document.FindAsync(FilterDefinition<LessonDocument>.Empty);
            var documents = task.ToEnumerable();
            return _mapper.Map<IEnumerable<Lesson>>(documents);
        }
        public async Task<Lesson?> GetByIdAsync(string id)
        {
            FilterDefinition<LessonDocument>? filter = Builders<LessonDocument>.Filter.Eq(x => x.Id, id);
            var task = await _document.FindAsync(filter);
            var document = task.FirstOrDefault();
            return _mapper.Map<Lesson>(document);
        }

        public async Task<Lesson> CreateAsync(Lesson entity)
        {
            var document = _mapper.Map<LessonDocument>(entity);
            
            // If order is null, auto-increment based on the last order for this course
            if (entity.Order == null)
            {
                // Filter by CourseId
                var filter = Builders<LessonDocument>.Filter.Eq(x => x.CourseId, entity.CourseId);
                
                // Sort by Order descending to get the last order
                var sort = Builders<LessonDocument>.Sort.Descending(x => x.Order);
                
                // Find the last lesson for this course
                var lastLesson = await _document.Find(filter)
                    .Sort(sort)
                    .Limit(1)
                    .FirstOrDefaultAsync();
                
                // Set order: if there's a previous lesson, increment its order, otherwise start at 1
                document.Order = lastLesson?.Order != null ? lastLesson.Order + 1 : 1;
                entity.Order = document.Order;
            }
            
            await _document.InsertOneAsync(document);
            entity.Id = document.Id;
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<LessonDocument>.Filter.Eq(x => x.Id, id);
            var result = await _document.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<Lesson> UpdateAsync(Lesson entity)
        {
            var filter = Builders<LessonDocument>.Filter.Eq(x => x.Id, entity.Id);
            var update = Builders<LessonDocument>.Update
                .Set(x => x.Title, entity.Title)
                .Set(x => x.Description, entity.Description)
                .Set(x => x.Order, entity.Order)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);
            
            await _document.UpdateOneAsync(filter, update);
            return entity;
        }

        public async Task<(IEnumerable<Lesson> lessons, long total)> GetPagedAsync(int page, int limit, string? courseId, string searchKey = "")
        {
            FilterDefinition<LessonDocument> filter = Builders<LessonDocument>.Filter.Empty;
            if (courseId != null)
            {
                filter = Builders<LessonDocument>.Filter.Eq(x => x.CourseId, courseId);
            }

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchFilter = Builders<LessonDocument>.Filter.Or(
                    Builders<LessonDocument>.Filter.Regex(x => x.Title, new MongoDB.Bson.BsonRegularExpression(searchKey, "i")),
                    Builders<LessonDocument>.Filter.Regex(x => x.Description, new MongoDB.Bson.BsonRegularExpression(searchKey, "i"))
                );
                filter = Builders<LessonDocument>.Filter.And(filter, searchFilter);
            }

            // Get total count
            var total = await _document.CountDocumentsAsync(filter);

            // Apply pagination
            var skip = (page - 1) * limit;
            var documents = await _document
                .Find(filter)
                .Sort(Builders<LessonDocument>.Sort.Ascending(x => x.Order))
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();

            var lessons = _mapper.Map<List<Lesson>>(documents);
            return (lessons, total);
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseId(string courseId)
        {
            var filter = Builders<LessonDocument>.Filter.Eq(x => x.CourseId, courseId);
            var documents = await _document
                .Find(filter)
                .Sort(Builders<LessonDocument>.Sort.Ascending(x => x.Order))
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<Lesson>>(documents);
        }

        public async Task<Lesson> CheckExistLessonInTheOrder(string CourseId, int Order)
        {
            FilterDefinition<LessonDocument> filter = Builders<LessonDocument>.Filter.And(
                Builders<LessonDocument>.Filter.Eq(x => x.CourseId, CourseId),
                Builders<LessonDocument>.Filter.Eq(x => x.Order, Order)
            );
            // alternative approach:
            //var filter = Builders<LessonDocument>.Filter.Eq(x => x.CourseId, CourseId) &
            // Builders<LessonDocument>.Filter.Eq(x => x.Order, Order);

            var task = await _document.FindAsync(filter);
            var document = task.FirstOrDefault();
            return _mapper.Map<Lesson>(document);
        }
    }
}
