

using Mango.Services.PaymentAPI.Services.Action;
using Mango.Services.PaymentAPI.Services.IAction;
using Mango.Services.PaymentAPI.Services.IServices;
using Mango.Services.PaymentAPI.Services.StepServices;
using Mango.Services.PaymentAPI.Services.StepServices.StepEnums;
using System.Diagnostics.CodeAnalysis;

namespace Mango.Services.ProductAPI.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.RegisterOrderProcessServices();
           //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
           //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
        public static IServiceCollection RegisterOrderProcessServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService,PaymentService>();
            services.AddScoped<IOrderService,OrderProcessingService>();

            services.AddScoped<Func<OrderProcessStep, IOrderService>>(servicesProvider => key =>
            {
                switch (key)
                {
                    case OrderProcessStep.ProcessOrder:
                        return servicesProvider.GetService<OrderProcessingService>()!;

                    case OrderProcessStep.ProcessPayment:
                        return servicesProvider.GetService<PaymentService>()!;

                    default:
                        return null;
                }
            });

            services.AddScoped<IOrderProcess, OrderProcess>();


            services.AddScoped<Func<OrderProcessStep, IOrderService>>(serviceProvider => orderStepKey =>
            {

                return serviceProvider.GetService<PaymentService>()!;
            });


            return services;

        }





    }

  

}
