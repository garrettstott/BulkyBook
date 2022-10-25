namespace Models.ViewModels;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductViewModel {
  public Product Product { get; set; }
  [ValidateNever]
  public IEnumerable<SelectListItem> Categories { get; set; }
  [ValidateNever]
  public IEnumerable<SelectListItem> CoverTypes { get; set; }
}
