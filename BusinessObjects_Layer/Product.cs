using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects_Layer;

public class Product
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a value")]
    public string Name { get; set; }

    [Required, MinLength(4, ErrorMessage = "Minimum length is 2")]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; }

    [ValidateNever]
    public string ImageURL { get; set; } = "noimage.png";
}
