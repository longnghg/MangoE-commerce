using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(double.MinValue,double.MaxValue)]
        public double Price { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(400)]
        public string ImageUrl { get; set; }
    }
}
