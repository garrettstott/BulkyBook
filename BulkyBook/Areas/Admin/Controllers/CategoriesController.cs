using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
  using DataAccess.Data;
  using DataAccess.Repository.IRepository;
  using Models;

  public class CategoriesController : Controller {

    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index() {
      IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
      return View(categories);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category) {
      if (ModelState.IsValid) {
       _unitOfWork.Category.Add(category);
       _unitOfWork.Save();
       return RedirectToAction("Index"); 
      }
      return View(category);
    }

    public IActionResult Edit(int? id) {
      var category = _unitOfWork.Category.GetFirstOrDefault(cat => cat.Id == id);
      if (category == null){
        return NotFound();
      }
      return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category) {
      if (ModelState.IsValid){
        _unitOfWork.Category.Update(category);
        _unitOfWork.Save();
        return RedirectToAction("Index");
      }
      return View(category);
    }

    [HttpPost] [ValidateAntiForgeryToken] 
    public IActionResult Delete(int? id) {
      var category = _unitOfWork.Category.GetFirstOrDefault(cat => cat.Id == id);
      if (category == null){
        return NotFound();
      }
      _unitOfWork.Category.Remove(category);
      _unitOfWork.Save();
      return RedirectToAction("Index");
    }
    
  }
}
