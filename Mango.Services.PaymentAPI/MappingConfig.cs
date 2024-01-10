using AutoMapper;
using Mango.Services.PaymentAPI.Models;
using Mango.Services.PaymentAPI.Models.Dtos;

namespace Mango.Services.PaymentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                //config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
