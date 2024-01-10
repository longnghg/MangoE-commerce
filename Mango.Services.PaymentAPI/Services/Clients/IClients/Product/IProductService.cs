using Mango.Services.PaymentAPI.Models.Dtos;

namespace Mango.Services.PaymentAPI.Services.Clients.IClients.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductDtosAsync();
    }
}
