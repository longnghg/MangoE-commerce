using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Services.Clients.IClients.Coupon
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponDtoAsync(string couponCode);
    }
}
