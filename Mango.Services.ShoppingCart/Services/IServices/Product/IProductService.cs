using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Services.IServices.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductDtosAsync();
    }
}
