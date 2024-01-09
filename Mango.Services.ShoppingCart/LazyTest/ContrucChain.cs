namespace Mango.Services.ShoppingCartAPI.LazyTest
{
    public class ContrucChain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContrucChain() : this("hoang","hoanglong")
        {
            int b = 123;

        }
        public ContrucChain(string name, string name2)
        {
            int a = 123;
        }
    }
}
