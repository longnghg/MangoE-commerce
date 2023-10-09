using Mango.Web.Service.IService;
using Mango.Web.Service;

namespace Mango.Web.Startup
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
