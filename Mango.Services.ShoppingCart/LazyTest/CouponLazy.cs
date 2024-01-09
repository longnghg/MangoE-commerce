namespace Mango.Services.ShoppingCartAPI.LazyTest
{
    public class CouponLazy
    {
        public int IdCouponLazy { get; set; }
        public string NameCoupontLazy { get; set; }
        public int AgeCouponLazy { get; set; }
        public string NameAfterDelay { get; set; }
        public CouponLazy()
        {
            IdCouponLazy = 01111;
            NameCoupontLazy = "Hoang Long";
            NameAfterDelay = GetNameAfterDelay();
        }
        public string GetNameAfterDelay()
        {
            Task.Delay(10000);
            return "Lazy hello";
        }

        public string Hello()
        {
            return "Lazy hello";
        }
    }
}
