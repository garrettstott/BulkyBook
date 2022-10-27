using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;

using DataAccess.Repository.IRepository;
using Models;
using Models.ViewModels;

[Area("Admin")]
public class OrdersController : Controller {
  
  private readonly IUnitOfWork _unitOfWork;

  public OrdersController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
    _unitOfWork = unitOfWork;

  }
  public IActionResult Index() {
    return View();
  }

  public IActionResult Details(int id) {
    var orderViewModel = new OrderViewModel() {
      OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(ord => ord.Id == id, includeProperties: "ApplicationUser"),
      OrderDetail = _unitOfWork.OrderDetail.GetAll(ord => ord.OrderId == id, includeProperties: "Product")
    };
    return View(orderViewModel);
  }
  
  #region API CALLS
  
  [HttpGet] public IActionResult GetALl(string? status) {
    IEnumerable<OrderHeader> orderHeaders;
    if (status == null){
      orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
    }
    else {
      orderHeaders = _unitOfWork.OrderHeader.GetAll(o=>o.OrderStatus==status, includeProperties: "ApplicationUser");
    }
    
    return Json(new { data = orderHeaders });
  }
    
  #endregion
  
}
