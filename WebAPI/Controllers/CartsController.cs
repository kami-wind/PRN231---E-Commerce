using BusinessObjects_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CartsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public IActionResult AddToCart([FromBody] Cart cart)
    {
        if (cart == null)
        {
            return BadRequest();
        }

        _unitOfWork.Cart.Add(cart);
        _unitOfWork.Save();

        return Ok(cart);
    }
}
