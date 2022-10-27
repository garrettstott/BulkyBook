namespace BulkyBook.ViewComponents;

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utility;

public class CartViewComponent : ViewComponent {
  private readonly IUnitOfWork _unitOfWork;

  public CartViewComponent(IUnitOfWork unitOfWork) {
    _unitOfWork = unitOfWork;
  }
  
  public async Task<IViewComponentResult> InvokeAsync() {
    var claimsIdentity = (ClaimsIdentity)User.Identity;
    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
    if (claim != null) {
      var session = HttpContext.Session.GetInt32(SD.SESSION_CART);
      if (session != null) {
        return View(session);
      } else {
        HttpContext.Session.SetInt32(
          SD.SESSION_CART,
          _unitOfWork.Cart.GetAll(c=>c.ApplicationUserId==claim.Value).Count()
        );
        return View(HttpContext.Session.GetInt32(SD.SESSION_CART));
      }
    } else {
      HttpContext.Session.Clear();
      return View(0);
    }
  }

}
