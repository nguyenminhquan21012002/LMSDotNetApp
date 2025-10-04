using Course.Core.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Core.Domain.Entities
{
    public class Courses
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("category")]
        public string? Category { get; set; }
        [BsonElement("level")]
        public string? Level { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement("status")]
        public int Status { get; set; }
    }
}
