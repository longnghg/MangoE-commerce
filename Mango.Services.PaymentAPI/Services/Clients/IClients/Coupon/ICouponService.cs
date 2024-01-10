using Mango.Services.PaymentAPI.Models.Dtos;

namespace Mango.Services.PaymentAPI.Services.Clients.IClients.Coupon
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponDtoAsync(string couponCode);
    }
}
