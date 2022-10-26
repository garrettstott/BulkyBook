using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Customer.Controllers;

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Models;
using Models.ViewModels;
using System.Security.Claims;

[Area("Customer")]
[Authorize]
public class CartController : Controller {

  private readonly IUnitOfWork _unitOfWork;
  public CartViewModel CartViewModel { get; set; }

  public CartController(IUnitOfWork unitOfWork) {
    _unitOfWork = unitOfWork;
  }
  
  public IActionResult Details() {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    CartViewModel = new CartViewModel()
    {
      ListCart = _unitOfWork.Cart.GetAll(
       cart => cart.ApplicationUserId == claim,
       includeProperties: "Product"
      )
    };
    foreach (var cart in CartViewModel.ListCart){
      CartViewModel.Total += (cart.Product.Price * cart.Count);
    }
    return View(CartViewModel);
  }

  [HttpPost] 
  [ValidateAntiForgeryToken] 
  public IActionResult Details(CartViewModel cartViewModel) {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    // foreach (var cart in cartViewModel.ListCart){
    //   _unitOfWork.Cart.UpdateCount(cart, cart.Count);
    // }
    // _unitOfWork.Save();
    return RedirectToAction("Details");
  }

  public IActionResult Remove() {
    return RedirectToAction("Details");
  }
  
  public IActionResult Summary() {
    return RedirectToAction("Details");
  }
}