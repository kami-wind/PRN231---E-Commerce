using BusinessObjects_Layer;
using BusinessObjects_Layer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepository;
using System.Net.Http.Headers;
using System.Text.Json;
using Web_Client.APIService;

namespace Web_Client.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{

    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        CategoryVM categoryVM = new CategoryVM();
        categoryVM.categories = await _categoryService.GetCategoriesAsync();
        //var categories = await _categoryService.GetCategoriesAsync();
        return View(categoryVM);
    }


    // GET: /Admin/Category/CreateUpdate/{id?}
    [HttpGet]
    public async Task<IActionResult> CreateUpdate(int? id)
    {
        CategoryVM categoryVM = new CategoryVM();

        if (id.HasValue)
        {
            // Edit existing category
            categoryVM.Category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (categoryVM.Category == null)
            {
                return NotFound();
            }
        }
        else
        {
            // Create new category
            categoryVM.Category = new Category();
        }

        return View(categoryVM);
    }


    // POST: /Admin/Category/CreateUpdate
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUpdate(CategoryVM categoryVM)
    {
        if (ModelState.IsValid)
        {
            if (categoryVM.Category.Id == 0)
            {
                // Create new category
                await _categoryService.CreateCategoryAsync(categoryVM.Category);
                TempData["success"] = "Category Created Done !";
            }
            else
            {
                // Update existing category
                await _categoryService.UpdateCategoryAsync(categoryVM.Category.Id, categoryVM.Category);
                TempData["success"] = "Category Updated Done !";
            }

            return RedirectToAction(nameof(Index));
        }

        return View(categoryVM);
    }


    //GET : /Admin/Category/Delete/{id}
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: /Admin/Category/Delete/{id}
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletedata(int id)
    {
        var category = _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        await _categoryService.DeleteCategoryAsync(id);
        TempData["success"] = "Category deleted Done !";

        return RedirectToAction(nameof(Index)); 
    }

}
