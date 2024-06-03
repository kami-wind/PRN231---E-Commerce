using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessObjects_Layer;
using Web_Client.APIService;
//using BusinessObjects_Layer.ViewModel;
using Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Web_Client.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly CartService _cartService;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, ProductService productService, CategoryService categoryService, CartService cartService, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _productService = productService;
        _categoryService = categoryService;
        _cartService = cartService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Detail(int id)
    {
        //var product = await _productService.GetProductByIdAsync(id);
        //if (product == null)
        //{
        //    return NotFound();
        //}

        //var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);

        //var productVM = new ProductVM
        //{
        //    Product = product,
        //    CategoryName = category?.Name
        //};

        //return View(productVM);
        Cart cart = new Cart()
        {
            Product = _unitOfWork.Product.GetT(x => x.Id == id,
            includeProperties: "Category"),
            Count =1,
            ProductId = id
        };

        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int productId, int count)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var cart = new Cart
        {
            ProductId = productId,
            ApplicationUserId = user.Id,
            Count = count
        };

        await _cartService.AddToCartAsync(cart);

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
