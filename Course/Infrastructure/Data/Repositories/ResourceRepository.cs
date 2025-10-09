using AutoMapper;
using Course.Core.Domain.Entities;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Course.Infrastructure.Data.MongoDbDocuments;
using MongoDB.Driver;

namespace Course.Infrastructure.Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IMongoCollection<ResourceDocument> _resources;
        private readonly IMapper _mapper;
        private readonly ILogger<ResourceRepository> _logger;

        public ResourceRepository(MongoDbContext context, IMapper mapper, ILogger<ResourceRepository> logger)
        {
            _resources = context.Database?.GetCollection<ResourceDocument>("resource");
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Resource>> GetByLessonIdAsync(string lessonId)
        {
            var filter = Builders<ResourceDocument>.Filter.Eq(x => x.LessonId, lessonId);
            var documents = await _resources.Find(filter).ToListAsync();
            return _mapper.Map<IEnumerable<Resource>>(documents);
        }

        public async Task<Resource?> GetByIdAsync(string id)
        {
            var filter = Builders<ResourceDocument>.Filter.Eq(x => x.Id, id);
            var document = await _resources.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<Resource>(document);
        }

        public async Task<Resource> CreateAsync(Resource resource)
        {
            var document = _mapper.Map<ResourceDocument>(resource);
            await _resources.InsertOneAsync(document);
            resource.Id = document.Id;
            return resource;
        }

        public async Task<Resource> UpdateAsync(Resource resource)
        {
            var filter = Builders<ResourceDocument>.Filter.Eq(x => x.Id, resource.Id);
            var update = Builders<ResourceDocument>.Update
                .Set(x => x.Title, resource.Title)
                .Set(x => x.Url, resource.Url)
                .Set(x => x.Type, (int)resource.Type)
                .Set(x => x.Metadata, resource.Metadata);

            await _resources.UpdateOneAsync(filter, update);
            return resource;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<ResourceDocument>.Filter.Eq(x => x.Id, id);
            var result = await _resources.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
