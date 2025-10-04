using Course.Core.Domain.Entities;
using Course.Core.Domain.Enums;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using MongoDB.Driver;

namespace Course.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IMongoCollection<Courses> _courses;
        
        public CourseRepository(MongoDbContext context)
        {
            _courses = context.Database?.GetCollection<Courses>("courses");
        }
        
        public async Task<IEnumerable<Courses>> GetAllAsync()
        {
            IAsyncCursor<Courses>? task = await _courses.FindAsync(FilterDefinition<Courses>.Empty);
            return task.ToEnumerable();
        }
        
        public async Task<Courses?> GetByIdAsync(string id)
        {
            FilterDefinition<Courses>? filter = Builders<Courses>.Filter.Eq(x => x.Id, id);
            IAsyncCursor<Courses>? task = await _courses.FindAsync(filter);
            return task.FirstOrDefault();
        }
        
        public async Task<Courses> CreateAsync(Courses course)
        {
            await _courses.InsertOneAsync(course);
            return course;
        }
        
        public async Task<Courses> UpdateAsync(Courses course)
        {
            var filter = Builders<Courses>.Filter.Eq(x => x.Id, course.Id);
            var update = Builders<Courses>.Update
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
            var filter = Builders<Courses>.Filter.Eq(x => x.Id, id);
            var result = await _courses.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
        
        public async Task<(IEnumerable<Courses> courses, long total)> GetPagedAsync(int page, int limit, string searchKey = "")
        {
            // Build filter for active courses
            var filter = Builders<Courses>.Filter.Eq(c => c.Status, 1);

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchFilter = Builders<Courses>.Filter.Or(
                    Builders<Courses>.Filter.Regex(c => c.Title, new MongoDB.Bson.BsonRegularExpression(searchKey, "i")),
                    Builders<Courses>.Filter.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchKey, "i"))
                );
                filter = Builders<Courses>.Filter.And(filter, searchFilter);
            }

            // Get total count
            var total = await _courses.CountDocumentsAsync(filter);

            // Apply pagination
            var skip = (page - 1) * limit;
            var courses = await _courses
                .Find(filter)
                .Sort(Builders<Courses>.Sort.Ascending(c => c.CreatedAt))
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();

            return (courses, total);
        }

        public async Task<int> GetTotalCountAsync(string searchKey = "")
        {
            // Build filter for active courses
            var filter = Builders<Courses>.Filter.Eq(c => c.Status, 1);

            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchFilter = Builders<Courses>.Filter.Or(
                    Builders<Courses>.Filter.Regex(c => c.Title, new MongoDB.Bson.BsonRegularExpression(searchKey, "i")),
                    Builders<Courses>.Filter.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchKey, "i"))
                );
                filter = Builders<Courses>.Filter.And(filter, searchFilter);
            }

            return (int)await _courses.CountDocumentsAsync(filter);
        }
    }
}
