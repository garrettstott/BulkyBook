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
  [Area("Admin")]
  public class CoverTypesController : Controller {

    private readonly IUnitOfWork _unitOfWork;

    public CoverTypesController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index() {
      IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();
      return View(coverTypes);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType coverType) {
      if (ModelState.IsValid) {
       _unitOfWork.CoverType.Add(coverType);
       _unitOfWork.Save();
       TempData["success"] = "Cover Type Created";
       return RedirectToAction("Index"); 
      }
      return View(coverType);
    }

    public IActionResult Edit(int? id) {
      var coverType = _unitOfWork.CoverType.GetFirstOrDefault(cover => cover.Id == id);
      if (coverType == null){
        return NotFound();
      }
      return View(coverType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType coverType) {
      if (ModelState.IsValid){
        _unitOfWork.CoverType.Update(coverType);
        _unitOfWork.Save();
        TempData["success"] = "Cover Type Updated";
        return RedirectToAction("Index");
      }
      return View(coverType);
    }

    [HttpPost] [ValidateAntiForgeryToken] 
    public IActionResult Delete(int? id) {
      var coverType = _unitOfWork.CoverType.GetFirstOrDefault(cover => cover.Id == id);
      if (coverType == null){
        return NotFound();
      }
      _unitOfWork.CoverType.Remove(coverType);
      _unitOfWork.Save();
      TempData["success"] = "Cover Type Deleted";
      return RedirectToAction("Index");
    }
    
  }
}
