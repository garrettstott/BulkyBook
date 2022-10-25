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
  public class CompaniesController : Controller {

    private readonly IUnitOfWork _unitOfWork;

    public CompaniesController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index() {
      return View();
    }

    public IActionResult Upsert(int? id) {
      var company = _unitOfWork.Company.GetFirstOrDefault(company => company.Id == id);
      if (id == null || id == 0){ company = new Company(); }
      if (company == null){
        return View(company);
        // New
      }
      else{
        // Edit
      }
      return View(company);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company company) {
      if (ModelState.IsValid){
        if (company.Id == 0){
          TempData["success"] = "Company Created";
          _unitOfWork.Company.Add(company);  
        }
        else {
          TempData["success"] = "Company Updated";
          _unitOfWork.Company.Update(company);
        }
        
        _unitOfWork.Save();
        return RedirectToAction("Index");
      }
      return View(company);
    }

    [HttpPost] 
    public IActionResult Delete(int? id) {
      var company = _unitOfWork.Company.GetFirstOrDefault(company => company.Id == id);
      if (company == null){
        return NotFound();
      }
      _unitOfWork.Company.Remove(company);
      _unitOfWork.Save();
      TempData["success"] = "Deleted";
      return RedirectToAction("Index");
    }
   
    #region API CALLS

    [HttpGet] public IActionResult GetALl() {
      var companies = _unitOfWork.Company.GetAll();
      return Json(new { data = companies });
    }
    
    #endregion
  }
}
