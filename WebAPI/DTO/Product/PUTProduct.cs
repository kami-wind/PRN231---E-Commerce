using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Product;

public class PUTProduct
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description must be less than 500 characters")]
    public string Description { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
    public int CategoryId { get; set; }

    [StringLength(2048, ErrorMessage = "Image URL must be less than 2048 characters")]
    public string ImageURL { get; set; } = "http://example.com/default-image.png";
}
