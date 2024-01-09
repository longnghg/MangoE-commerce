using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Services.IServices.Coupon
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponDtoAsync(string couponCode);
    }
}
