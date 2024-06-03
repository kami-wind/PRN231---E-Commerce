using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
namespace BusinessObjects_Layer;

public class Cart
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    [ValidateNever]
    public Product Product { get; set; }

    [ValidateNever]
    public string ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public int Count { get; set; }
}
