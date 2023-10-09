

namespace Mango.Services.ProductAPI.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
           //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
           //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
