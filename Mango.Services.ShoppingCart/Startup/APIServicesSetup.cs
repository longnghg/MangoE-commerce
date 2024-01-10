using Mango.Services.ShoppingCartAPI.Services.Clients;
using Mango.Services.ShoppingCartAPI.Services.Clients.IClients.Coupon;
using Mango.Services.ShoppingCartAPI.Services.Clients.IClients.Product;

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
