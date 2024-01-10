using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Services.Clients.IClients.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductDtosAsync();
    }
}
