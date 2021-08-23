using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Title is required")]
        [MaxLength(60,ErrorMessage = "Product Title must contain between 3 and 60 characters.")]
        [MinLength(3,ErrorMessage = "Product Title must contain between 3 and 60 characters.")]
        public string Title { get; set; }
        [MaxLength(1024,ErrorMessage= "The Product Description can contain up to 1024 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The Product Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The product price must be greater than zero")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Category of this Product is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}