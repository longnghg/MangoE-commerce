using Mango.Services.ShoppingCartAPI.Services;
using Mango.Services.ShoppingCartAPI.Services.IServices.Coupon;
using Mango.Services.ShoppingCartAPI.Services.IServices.Product;

namespace Mango.Services.ShoppingCartAPI.Startup
{
    // Register DI of other service to call api
    public static class APIServicesSetup
    {
        public static IServiceCollection RegisterAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICouponService, CouponService>();
            return services;
        }
    }
}
