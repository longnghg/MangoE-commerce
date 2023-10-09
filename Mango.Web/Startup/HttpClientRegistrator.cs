using Mango.Web.Service.IService;
using Mango.Web.Service;

namespace Mango.Web.Startup
{
    public static class HttpClientRegistrator
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddHttpClient<IProductService, ProductService>();
            return services;
        }

    }
}
