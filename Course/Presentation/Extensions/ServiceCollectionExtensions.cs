using Course.Core.Application.Mappings;
using Course.Core.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Course.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            
            // AutoMapper
            services.AddAutoMapper(typeof(CourseMappingProfile));

            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            
            // Controllers
            services.AddControllers();
            
            return services;
        }
    }
}
