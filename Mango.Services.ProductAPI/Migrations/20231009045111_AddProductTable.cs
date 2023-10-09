using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Meet", "Product test", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcToVNdR-ao3wS7o5AmdkilhQ2zdqOmQ4TSJsQ&usqp=CAU", "Test", 15.0 },
                    { 2, "Appetizer", "Product test", "https://c8.alamy.com/comp/R0P6RH/supermarket-grocery-products-cartoon-R0P6RH.jpg", "Test 2", 13.99 },
                    { 3, "Desert", "Product test", "https://cdn1.vectorstock.com/i/1000x1000/33/10/supermarket-products-cartoon-vector-22713310.jpg", "Test 3", 3.9900000000000002 },
                    { 4, "Entree", "Product test", "https://cdn2.vectorstock.com/i/1000x1000/33/06/supermarket-products-cartoon-vector-22713306.jpg", "Test 4", 23.989999999999998 },
                    { 5, "Drink", "Product test", "https://www.shutterstock.com/shutterstock/photos/2204181003/display_1500/stock-vector-cosmetic-tube-with-abstract-floral-background-skincare-product-packaging-template-hand-drawn-2204181003.jpg", "Test 5", 5.9900000000000002 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
