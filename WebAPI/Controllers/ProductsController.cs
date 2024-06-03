using BusinessObjects_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using WebAPI.DTO.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    [HttpGet]
    public IActionResult GetAllProduct()
    {
        var products = _unitOfWork.Product.GetAll();


        // Map Product entities to ProductDto
        var productDtos = products.Select(p => new GETProduct
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = _unitOfWork.Category.GetT(c => c.Id == p.CategoryId)?.Name, // Get category name
            ImageURL = p.ImageURL
            // Set other properties as needed
        });

        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id) 
    {
        var product = _unitOfWork.Product.GetT(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        // Map Product entity to ProductDto
        var productDto = new GETProduct
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = _unitOfWork.Category.GetT(c => c.Id == product.CategoryId)?.Name,
            ImageURL = product.ImageURL
        };

        return Ok(productDto);
    }

    [HttpPost]
    public IActionResult CreateProduct(POSTProduct productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.ImageURL))
        {
            productDto.ImageURL = "http://example.com/noimage.png"; // Default image URL
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            ImageURL = productDto.ImageURL
        };

        _unitOfWork.Product.Add(product);
        _unitOfWork.Save();

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, PUTProduct productDto)
    {
        var existingProduct = _unitOfWork.Product.GetT(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        // Update existing product properties
        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.Price = productDto.Price;
        existingProduct.CategoryId = productDto.CategoryId;
        existingProduct.ImageURL = productDto.ImageURL;

        _unitOfWork.Product.Update(existingProduct);
        _unitOfWork.Save();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existingProduct = _unitOfWork.Product.GetT(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        _unitOfWork.Product.Delete(existingProduct);
        _unitOfWork.Save();

        return NoContent();
    }
}
