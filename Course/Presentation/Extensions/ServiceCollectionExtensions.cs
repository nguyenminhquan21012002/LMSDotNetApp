using Course.Core.Application.Mappings;
using Course.Core.Application.Validators;
using Course.Core.Domain.Interfaces;
using Course.Infrastructure.Data.DbContexts;
using Course.Infrastructure.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Course.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<CourseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            
            // AutoMapper
            services.AddAutoMapper(typeof(CourseMappingProfile));
            
            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            
            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            
            // Controllers
            services.AddControllers();
            
            return services;
        }
    }
}
