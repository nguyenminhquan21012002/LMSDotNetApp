using Quiz.Core.Domain.Interfaces;
using Quiz.Infrastructure.Data.DbContexts;
using Quiz.Infrastructure.Data.Mappings;
using Quiz.Infrastructure.Data.Repositories;

namespace Quiz.Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB
            services.AddSingleton<MongoDbContext>();

            // Repositories
            services.AddScoped<IQuizRepository, QuizRepository>();

            // AutoMapper
            services.AddAutoMapper(typeof(QuizDocumentMappingProfile));

            return services;
        }
    }
}

