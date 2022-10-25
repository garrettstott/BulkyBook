using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
  using DataAccess.Data;
  using DataAccess.Repository.IRepository;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Models;
  using Models.ViewModels;
  [Area("Admin")]
  public class ProductsController : Controller {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) {
      _unitOfWork = unitOfWork;
      _hostEnvironment = hostEnvironment;
    }
    public IActionResult Index() {
      return View();
    }

    // public IActionResult Create() {
    //   return View();
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Create(Product product) {
    //   if (ModelState.IsValid) {
    //    _unitOfWork.Product.Add(product);
    //    _unitOfWork.Save();
    //    return RedirectToAction("Index"); 
    //   }
    //   return View(product);
    // }

    public IActionResult Upsert(int? id) {
      var product = _unitOfWork.Product.GetFirstOrDefault(product => product.Id == id);
      if (id == null || id == 0){ product = new Product(); }
      ProductViewModel productViewModel = new() {
        Product = product,
        Categories = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
        {
          Text = i.Name,
          Value = i.Id.ToString()
        }),
        CoverTypes = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
        {
          Text = i.Name,
          Value = i.Id.ToString()
        })
      };
      
      if (product == null){
        return View(productViewModel);
        // New
      }
      else{
        // Edit
      }
      return View(productViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file) {
      if (ModelState.IsValid){
        string wwwRootPath = _hostEnvironment.WebRootPath;
        if (file != null){
          string fileName = Guid.NewGuid().ToString();
          var uploads = Path.Combine(wwwRootPath, @"images/products/");
          var extention = Path.GetExtension(file.FileName);
          var fullFileName = fileName + extention;
          if (productViewModel.Product.ImageUrl != null){
            var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath)){
              System.IO.File.Delete(oldImagePath);
            }
          }
          using (var fileStreams = new FileStream(Path.Combine(uploads, fullFileName), FileMode.Create)){
            file.CopyTo(fileStreams);
          }
          productViewModel.Product.ImageUrl = @"/images/products/" + fullFileName;
        }
        if (productViewModel.Product.Id == 0){
          _unitOfWork.Product.Add(productViewModel.Product);  
        }
        else {
          _unitOfWork.Product.Update(productViewModel.Product);
        }
        
        _unitOfWork.Save();
        return RedirectToAction("Index");
      }
      return View(productViewModel);
    }

    [HttpPost] 
    public IActionResult Delete(int? id) {
      var product = _unitOfWork.Product.GetFirstOrDefault(product => product.Id == id);
      if (product == null){
        return NotFound();
      }
      string wwwRootPath = _hostEnvironment.WebRootPath;
      var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('/'));
      if (System.IO.File.Exists(oldImagePath)){
        System.IO.File.Delete(oldImagePath);
      }
      _unitOfWork.Product.Remove(product);
      _unitOfWork.Save();
      return RedirectToAction("Index");
    }
   
    #region API CALLS

    [HttpGet] public IActionResult GetALl() {
      var products = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
      return Json(new { data = products });
    }
    
    #endregion
  }
}
