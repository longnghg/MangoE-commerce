namespace Mango.Services.PaymentAPI.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public int MinAmount { get; set; }
        public double DiscountAmount { get; set; }


    }
}
