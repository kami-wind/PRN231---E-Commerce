using BusinessObjects_Layer;
using BusinessObjects_Layer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using Web_Client.APIService;

namespace Web_Client.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public ProductController(ProductService productService, CategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();

        // For each product, fetch the category name from the API
        foreach (var product in products)
        {
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category != null)
            {
                // Instantiate the product.Category if it's null
                product.Category ??= new Category();
                // Assign the category name to product.Category.Name
                product.Category.Name = category.Name;
            }
        }

        var productViewModels = products.Select(p => new ProductVM
        {
            Product = p,
            CategoryName = p.Category?.Name // Assign the category name
        });

        return View(productViewModels);
    }

    public async Task<IActionResult> CreateUpdate(int? id)
    {
        var productVM = new ProductVM();

        if (id.HasValue)
        {
            // If id is provided, it's an update operation
            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product != null)
            {
                productVM.Product = product;
                // Fetch category name if needed
                var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
                productVM.CategoryName = category?.Name;
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            // If id is not provided, it's a create operation
            productVM.Product = new Product();
        }
        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = categories;

        return View(productVM);
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> CreateUpdateConfirm(ProductVM productVM)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        var categories = await _categoryService.GetCategoriesAsync();
    //        ViewBag.Categories = categories;
    //        return View("CreateUpdate", productVM);
    //    }

    //    if (string.IsNullOrWhiteSpace(productVM.Product.ImageURL))
    //    {
    //        productVM.Product.ImageURL = "http://example.com/default-image.png"; // Default image URL
    //    }

    //    if (productVM.Product.Id == 0)
    //    {
    //        await _productService.CreateProductAsync(productVM.Product);
    //    }
    //    else
    //    {
    //        await _productService.UpdateProductAsync(productVM.Product.Id, productVM.Product);
    //    }

    //    return RedirectToAction(nameof(Index));
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUpdateConfirm(ProductVM productVM)
    {
        if (!ModelState.IsValid)
        {
            return View(productVM);
        }

        if (productVM.ImageFile != null && productVM.ImageFile.Length > 0)
        {
            var fileName = Path.GetFileName(productVM.ImageFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productVM.ImageFile.CopyToAsync(stream);
            }

            productVM.Product.ImageURL = "/images/" + fileName;
        }
        else
        {
            // If no new file is uploaded, ensure the existing URL is preserved
            var existingProduct = await _productService.GetProductByIdAsync(productVM.Product.Id);
            if (existingProduct != null)
            {
                productVM.Product.ImageURL = existingProduct.ImageURL;
            }
        }

        if (productVM.Product.Id > 0)
        {
            await _productService.UpdateProductAsync(productVM.Product.Id, productVM.Product);
        }
        else
        {
            await _productService.CreateProductAsync(productVM.Product);
        }

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
        product.Category = new Category { Name = category?.Name };
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}



