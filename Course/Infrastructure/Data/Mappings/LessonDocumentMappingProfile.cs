using AutoMapper;
using Course.Core.Domain.Entities;
using Course.Infrastructure.Data.MongoDbDocuments;

namespace Course.Infrastructure.Data.Mappings
{
    public class LessonDocumentMappingProfile: Profile
    {
        public LessonDocumentMappingProfile() {
            // Map from domain entity to MongoDB document
            CreateMap<Lesson, LessonDocument>().ReverseMap();
        }
    }
}
