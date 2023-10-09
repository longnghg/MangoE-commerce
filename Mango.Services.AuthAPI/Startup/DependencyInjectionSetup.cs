using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.AuthAPI.Service;

namespace Mango.Services.AuthAPI.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
           services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
           services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
