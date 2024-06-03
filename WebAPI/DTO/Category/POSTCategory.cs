using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Category;

public class POSTCategory
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; }
}
