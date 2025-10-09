using MongoDB.Bson.Serialization.Attributes;

namespace Course.Core.Domain.Entities
{
    public class Metadata
    {
        [BsonElement("duration")]
        public int? Duration { get; set; }
        
        [BsonElement("filesize")]
        public string? FileSize { get; set; }
        
        [BsonElement("resolution")]
        public string? Resolution { get; set; }
        
        [BsonElement("pages")]
        public int? Pages { get; set; }
        
        [BsonElement("source")]
        public string? Source { get; set; }
    }
}
