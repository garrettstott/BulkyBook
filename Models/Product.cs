namespace Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

public class Product {
  [Key]
  public int Id { get; set; }
  
  [Required]
  public string Name { get; set; }
  
  [Required]
  public string Author { get; set; }
  
  [Required]
  public string Description { get; set; }
  
  [Required]
  [Display(Name="List Price")]
  public double ListPrice { get; set; }
  
  [Required]
  public double Price { get; set; }
  
  [ValidateNever]
  [Display(Name="Image")]
  public string ImageUrl { get; set; }
  
  [Display(Name="Category")]
  public int CategoryId { get; set; }
  
  [ValidateNever]
  public Category Category { get; set; }
  
  [Display(Name="CoverType")]
  public int CoverTypeId { get; set; }
  
  [ValidateNever]
  public CoverType CoverType { get; set; }
}
