using AutoMapper;

namespace Mango.Services.ProductAPI.Startup
{
    public static class AutomapperSetup
    {
        // Mapper
        static IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        public static IServiceCollection RegisterAutoMapperServices(this IServiceCollection services)
        {
            services.AddSingleton(mapper);
            return services;
        }
    }
}
