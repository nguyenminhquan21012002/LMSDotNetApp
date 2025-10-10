using LMSApp.Shared.Middleware;
using Quiz.Infrastructure.Data.Extensions;
using Quiz.Presentation.Extensions;

namespace Quiz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services using extension methods
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

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
            app.MapGet("/ping", () => Results.Ok(new { service = "quiz", ok = true }));

            app.Run("http://localhost:5005");
        }
    }
}
