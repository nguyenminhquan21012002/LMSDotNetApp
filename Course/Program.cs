using Course.Infrastructure.Data.DbContexts;
using Course.Presentation.Extensions;
using LMSApp.Shared.Middleware;

namespace Course
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services using extension method
            builder.Services.AddApplicationServices(builder.Configuration);
            
            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add global exception handling
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();
            app.MapControllers();
            
            // Health check endpoint
            app.MapGet("/ping", () => Results.Ok(new { service = "course", ok = true }));

            app.Run("http://localhost:5002");
        }
    }
}