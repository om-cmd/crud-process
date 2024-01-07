using System.ComponentModel.DataAnnotations;

namespace shopping.Models
{
   

public class Product
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Product name should include alphabets only.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000.")]
        public float Price { get; set; }
     
    }

}

