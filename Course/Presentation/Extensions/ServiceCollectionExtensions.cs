using Course.Core.Application.Mappings;
using Course.Core.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

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
            services.AddAutoMapper(typeof(LessonMappingProfile));
            services.AddAutoMapper(typeof(ResourceMappingProfile));

            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();

            // Controllers
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    Dictionary<string, string[]> errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return new BadRequestObjectResult(new
                    {
                        status = "error",
                        message = "Validation failed",
                        data = (object)null,
                        error = new
                        {
                            errorCode = 1001,
                            errorMessage = string.Join(", ", errors.SelectMany(x => x.Value))
                        }
                    });
                };
            });

            return services;
        }
    }
}
