
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Infrastructure.Data;
using Identity.Presentation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Controllers + Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // MediatR
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // DI from Infrastructure
            builder.Services.AddIdentityDependencies();

            var app = builder.Build();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers(); // 👈 bật Controllers
            app.MapGet("/ping", () => Results.Ok(new { service = "identity", ok = true }));
            app.MapGet("/healthz", () => "ok");

            app.Run("http://localhost:5001");

        }
    }
}
