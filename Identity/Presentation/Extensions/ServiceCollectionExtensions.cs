using Identity.Core.Domain.Interfaces;
using Identity.Infrastructure.Data;

namespace Identity.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
