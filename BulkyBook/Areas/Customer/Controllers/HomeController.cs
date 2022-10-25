using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Customer.Controllers;

using DataAccess.Repository.IRepository;
using Models;

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

  public IActionResult Details(int? id) {
    Cart cart = new() {
      Count=1,
      Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType"),
    };
    return View(cart);
  }

  public IActionResult Privacy() {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
