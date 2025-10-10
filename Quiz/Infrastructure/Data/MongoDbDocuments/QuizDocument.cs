using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Quiz.Infrastructure.Data.MongoDbDocuments
{
    public class QuizDocument
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("courseId"), BsonRepresentation(BsonType.ObjectId)]
        public string? CourseId { get; set; }
        
        [BsonElement("title")]
        public string? Title { get; set; }
        
        [BsonElement("description")]
        public string? Description { get; set; }
        
        [BsonElement("type")]
        public int Type { get; set; }
        
        [BsonElement("timeLimit")]
        public int? TimeLimit { get; set; } // in seconds
        
        [BsonElement("totalPoints")]
        public double? TotalPoints { get; set; }
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement("createdBy")]
        public string? CreatedBy { get; set; }
    }
}
