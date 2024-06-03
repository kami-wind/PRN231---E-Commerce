using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects_Layer.ViewModel;

public class ProductVM
{
    public Product Product { get; set; }
    public string CategoryName { get; set; }

    public IFormFile ImageFile { get; set; }
}
