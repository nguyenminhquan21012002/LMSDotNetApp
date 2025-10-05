using AutoMapper;
using Course.Core.Application.DTOs;
using Course.Core.Domain.Entities;
using Course.Core.Domain.Enums;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Course.Infrastructure.Data.MongoDbDocuments;
using MongoDB.Driver;

namespace Course.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IMongoCollection<CourseDocument> _courses;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseRepository> _logger;
        public CourseRepository(MongoDbContext context, IMapper mapper, ILogger<CourseRepository> logger)
        {
            _courses = context.Database?.GetCollection<CourseDocument>("courses");
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Courses>> GetAllAsync()
        {
            IAsyncCursor<CourseDocument>? task = await _courses.FindAsync(FilterDefinition<CourseDocument>.Empty);
            IEnumerable<CourseDocument> courseDocuments = task.ToEnumerable();
            return _mapper.Map<IEnumerable<Courses>>(courseDocuments);
        }
        
        public async Task<Courses?> GetByIdAsync(string id)
        {
            FilterDefinition<CourseDocument>? filter = Builders<CourseDocument>.Filter.Eq(x => x.Id, id);
            IAsyncCursor<CourseDocument>? task = await _courses.FindAsync(filter);
            CourseDocument document = task.FirstOrDefault();
            return _mapper.Map<Courses>(document);
        }
        
        public async Task<Courses> CreateAsync(Courses course)
        {
            var document = _mapper.Map<CourseDocument>(course);
            await _courses.InsertOneAsync(document);
            course.Id = document.Id;
            return course;
        }

        public async Task<Courses> UpdateAsync(Courses course)
        {
            var filter = Builders<CourseDocument>.Filter.Eq(x => x.Id, course.Id);
            _logger.LogInformation("check filter " + filter, "Check UpdateAsync Filter");
            var update = Builders<CourseDocument>.Update
                .Set(x => x.Title, course.Title)
                .Set(x => x.Description, course.Description)
                .Set(x => x.Category, course.Category)
                .Set(x => x.Level, course.Level)
                .Set(x => x.Status, course.Status)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);
            await _courses.UpdateOneAsync(filter, update);
            return course;
        }
        
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<CourseDocument>.Filter.Eq(x => x.Id, id);
            var result = await _courses.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
        
        public async Task<(IEnumerable<Courses> courses, long total)> GetPagedAsync(int page, int limit, string searchKey = "")
        {
            // Build filter for active courses
            FilterDefinition<CourseDocument> filter = Builders<CourseDocument>.Filter.Empty;

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchFilter = Builders<CourseDocument>.Filter.Or(
                    Builders<CourseDocument>.Filter.Regex(c => c.Title, new MongoDB.Bson.BsonRegularExpression(searchKey, "i")),
                    Builders<CourseDocument>.Filter.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchKey, "i"))
                );
                filter = Builders<CourseDocument>.Filter.And(filter, searchFilter);
            }

            // Get total count
            var total = await _courses.CountDocumentsAsync(filter);

            // Apply pagination
            var skip = (page - 1) * limit;
            List<CourseDocument> courseDocument = await _courses
                .Find(filter)
                .Sort(Builders<CourseDocument>.Sort.Ascending(c => c.CreatedAt))
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();

            List<Courses> courses = _mapper.Map<List<Courses>>(courseDocument);
            return (courses, total);
        }

        public async Task<int> GetTotalCountAsync(string searchKey = "")
        {
            // Build filter for active courses
            FilterDefinition<CourseDocument> filter = Builders<CourseDocument>.Filter.Empty;

            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchFilter = Builders<CourseDocument>.Filter.Or(
                    Builders<CourseDocument>.Filter.Regex(c => c.Title, new MongoDB.Bson.BsonRegularExpression(searchKey, "i")),
                    Builders<CourseDocument>.Filter.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchKey, "i"))
                );
                filter = Builders<CourseDocument>.Filter.And(filter, searchFilter);
            }

            return (int)await _courses.CountDocumentsAsync(filter);
        }
    }
}
