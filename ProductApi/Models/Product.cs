using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string ImageUrl { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int Stock { get; set; }
    }
}
