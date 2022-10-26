using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Customer.Controllers;

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using Models.ViewModels;
using System.Security.Claims;
using Utility;

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
      ListCart = _unitOfWork.Cart.GetAll(cart => cart.ApplicationUserId == claim, includeProperties: "Product").ToList(),
      OrderHeader = new()
    };
    foreach (var cart in CartViewModel.ListCart){
      CartViewModel.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
    }
    return View(CartViewModel);
  }

  [HttpPost] 
  [ValidateAntiForgeryToken] 
  public IActionResult Details(List<Cart> listCart) {
    foreach (var cart in listCart){
      Cart existingCart = _unitOfWork.Cart.GetFirstOrDefault(c => c.Id == cart.Id);
      _unitOfWork.Cart.UpdateCount(existingCart, cart.Count);
    }
    _unitOfWork.Save();
    return RedirectToAction("Details");
  }

  public IActionResult Remove() {
    return RedirectToAction("Details");
  }
  
  public IActionResult Summary() {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    CartViewModel = new CartViewModel()
    {
      ListCart = _unitOfWork.Cart.GetAll(cart => cart.ApplicationUserId == claim, includeProperties: "Product").ToList(),
      OrderHeader = new()
    };
    var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim);
    CartViewModel.OrderHeader.ApplicationUser = applicationUser;
    CartViewModel.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
    CartViewModel.OrderHeader.StreetAddress = applicationUser.StreetAddress;
    CartViewModel.OrderHeader.City = applicationUser.City;
    CartViewModel.OrderHeader.State = applicationUser.State;
    CartViewModel.OrderHeader.PostalCode = applicationUser.PostalCode;
    foreach (var cart in CartViewModel.ListCart){
      CartViewModel.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
    }
    return View(CartViewModel);
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  [ActionName("Summary")]
  public IActionResult SummaryPost(CartViewModel CartViewModel) {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    CartViewModel.ListCart = _unitOfWork.Cart.GetAll(cart => cart.ApplicationUserId == claim, includeProperties: "Product").ToList();
    CartViewModel.OrderHeader.OrderStatus = SD.STATUS_SHIPPED;
    CartViewModel.OrderHeader.OrderDate = DateTime.Now;
    CartViewModel.OrderHeader.ApplicationUserId = claim;
    CartViewModel.OrderHeader.ShippingDate = DateTime.Now;
    // FAKE ALL THE THINGS
    CartViewModel.OrderHeader.TrackingNumber = Guid.NewGuid().ToString();
    CartViewModel.OrderHeader.SessionId = Guid.NewGuid().ToString();
    CartViewModel.OrderHeader.PaymentId =Guid.NewGuid().ToString();
    
    foreach (var cart in CartViewModel.ListCart){
      CartViewModel.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
    }
    _unitOfWork.OrderHeader.Add(CartViewModel.OrderHeader);
    _unitOfWork.Save();
    
    foreach (var cart in CartViewModel.ListCart){
      OrderDetail orderDetail = new()
      {
        ProductId = cart.ProductId,
        OrderId = CartViewModel.OrderHeader.Id,
        Price = cart.Product.Price,
        Count = cart.Count
      };
      _unitOfWork.OrderDetail.Add(orderDetail);
      _unitOfWork.Save();
    }
    _unitOfWork.Cart.RemoveRange(CartViewModel.ListCart);
    _unitOfWork.Save();
    return RedirectToAction("Index", "Home");
  }
}