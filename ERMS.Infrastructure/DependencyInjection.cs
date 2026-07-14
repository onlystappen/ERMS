using ERMS.Infrastructure.Authentication;
using ERMS.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ERMS.Application.Services;

namespace ERMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            
            services.AddScoped<AuthService>();

            
            services.AddScoped<DepartmentService>();

            
            services.AddScoped<RequestService>();


            
            return services;
        }
    }
}