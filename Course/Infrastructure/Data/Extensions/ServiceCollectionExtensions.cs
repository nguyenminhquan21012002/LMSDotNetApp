using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Course.Infrastructure.Data.Mappings;
using Course.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Course.Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<CourseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // MongoDB
            services.AddSingleton<MongoDbContext>();

            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();

            // Auto Mapper
            services.AddAutoMapper(typeof(CourseDocumentMappingProfile));
            services.AddAutoMapper(typeof(LessonDocumentMappingProfile));

            return services;
        }
    }
}
