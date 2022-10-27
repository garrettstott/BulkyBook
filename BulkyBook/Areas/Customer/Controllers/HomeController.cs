using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Customer.Controllers;

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models;
using System.Security.Claims;
using Utility;

[Area("Customer")]
public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  private readonly IUnitOfWork _unitOfWork;

  public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) {
    _logger = logger;
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index() {
    IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
    return View(products);
  }

  public IActionResult Details(int productId) {
    Cart cart = new() {
      Count = 1,
      ProductId = productId,
      Product = _unitOfWork.Product.GetFirstOrDefault(product => product.Id == productId, includeProperties: "Category,CoverType"),
    };
    return View(cart);
  }

  [HttpPost] 
  [ValidateAntiForgeryToken] 
  [Authorize] 
  public IActionResult Details(Cart cart) {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    Cart existingCart = _unitOfWork.Cart.GetFirstOrDefault(
    f => f.ApplicationUserId == claim
         &&
         f.ProductId == cart.ProductId
    );
    if (existingCart == null){
      cart.ApplicationUserId = claim;
      _unitOfWork.Cart.Add(cart);
    }
    else{
      _unitOfWork.Cart.UpdateCount(existingCart, existingCart.Count+cart.Count);
      _unitOfWork.Save();
    }
    _unitOfWork.Save();
    HttpContext.Session.SetInt32(
      SD.SESSION_CART,
      _unitOfWork.Cart.GetAll(c=>c.ApplicationUserId==claim).Count()
    );
    TempData["success"] = "Added to cart";
    return RedirectToAction("Details", "Cart");
  }

  public IActionResult Privacy() {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
