using ERMS.Infrastructure.Authentication;
using ERMS.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ERMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }


    }
}