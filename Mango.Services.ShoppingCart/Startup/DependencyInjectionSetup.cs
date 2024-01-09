

namespace Mango.Services.ShoppingCartAPI.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterDIServices(this IServiceCollection services)
        {
           //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
           //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
