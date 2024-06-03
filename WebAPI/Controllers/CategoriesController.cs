using BusinessObjects_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using WebAPI.DTO.Category;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public CategoriesController (IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAllCategory()
    {
        var categories = _unitOfWork.Category.GetAll();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id) 
    {
        var existingCategory = _unitOfWork.Category.GetT(c => c.Id == id);
        if (existingCategory == null)
        {
            return NotFound();
        }
        return Ok(existingCategory);
    }

    [HttpPost]
    public IActionResult CreateCategory(POSTCategory categoryDto)
    {
        // Map CategoryDto to Category entity
        var category = new Category
        {
            Name = categoryDto.Name,
        };

        _unitOfWork.Category.Add(category);
        _unitOfWork.Save();

        //return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDto);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, POSTCategory categoryDto)
    {
        var existingCategory = _unitOfWork.Category.GetT(c => c.Id == id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        // Update existing category properties
        existingCategory.Name = categoryDto.Name;

        _unitOfWork.Category.Update(existingCategory);
        _unitOfWork.Save();

        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var existingCategory = _unitOfWork.Category.GetT(c => c.Id == id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Delete(existingCategory);
        _unitOfWork.Save();

        return NoContent();
    }
}
