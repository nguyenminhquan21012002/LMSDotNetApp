using Course.Core.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Infrastructure.Data.MongoDbDocuments
{
    public class ResourceDocument
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("lesson_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? LessonId { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("url")]
        public string? Url { get; set; }
        [BsonElement("type")]
        public int? Type { get; set; }
        [BsonElement("metadata")]
        public Metadata? Metadata { get; set; }
    }
}
