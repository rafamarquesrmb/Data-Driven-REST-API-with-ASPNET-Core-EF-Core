using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Title is required")]
        [MaxLength(60,ErrorMessage = "Category Title must contain between 3 and 60 characters.")]
        [MinLength(3,ErrorMessage = "Category Title must contain between 3 and 60 characters.")]
        public string Title { get; set; }
    }
}