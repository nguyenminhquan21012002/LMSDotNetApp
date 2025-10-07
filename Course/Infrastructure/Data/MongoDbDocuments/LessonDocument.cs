using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Infrastructure.Data.MongoDbDocuments
{
    public class LessonDocument
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("course_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? CourseId { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("order")]
        public int? Order { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
