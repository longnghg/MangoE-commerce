using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Test",
                Price = 15,
                Description = "Product test",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcToVNdR-ao3wS7o5AmdkilhQ2zdqOmQ4TSJsQ&usqp=CAU",
                CategoryName = "Meet"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Test 2",
                Price = 13.99,
                Description = "Product test",
                ImageUrl = "https://c8.alamy.com/comp/R0P6RH/supermarket-grocery-products-cartoon-R0P6RH.jpg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Test 3",
                Price = 3.99,
                Description = "Product test",
                ImageUrl = "https://cdn1.vectorstock.com/i/1000x1000/33/10/supermarket-products-cartoon-vector-22713310.jpg",
                CategoryName = "Desert"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Test 4",
                Price = 23.99,
                Description = "Product test",
                ImageUrl = "https://cdn2.vectorstock.com/i/1000x1000/33/06/supermarket-products-cartoon-vector-22713306.jpg",
                CategoryName = "Entree"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Test 5",
                Price = 5.99,
                Description = "Product test",
                ImageUrl = "https://www.shutterstock.com/shutterstock/photos/2204181003/display_1500/stock-vector-cosmetic-tube-with-abstract-floral-background-skincare-product-packaging-template-hand-drawn-2204181003.jpg",
                CategoryName = "Drink"
            });
        }
    }
}
