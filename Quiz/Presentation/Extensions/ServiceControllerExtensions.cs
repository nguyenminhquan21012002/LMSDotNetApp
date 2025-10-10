using Quiz.Core.Application.Mappings;

namespace Quiz.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // AutoMapper
            services.AddAutoMapper(typeof(QuizMappingProfile));

            // Controllers
            services.AddControllers();
            
            return services;
        }
    }
}
