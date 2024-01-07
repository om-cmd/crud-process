
using shopping.Models;
using System.ComponentModel.DataAnnotations;

public class Sale
{
    public int? Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }

    [Required(ErrorMessage = "quantity is required.")]
    [Range(100, double.MaxValue, ErrorMessage = "quantity must be greater than 0.")]
    public float Quantity { get; set; }

    public float TotalPrice {  get; set; }
  
}
