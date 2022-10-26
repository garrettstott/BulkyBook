namespace Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cart {
  [Key]
  public int Id { get; set; }
  public Product Product { get; set; }
  public int Count { get; set; }
  
  public int ProductId { get; set; }
  [ForeignKey("ProductId")]
  [ValidateNever]
  public string ApplicationUserId { get; set; }
  [ForeignKey("ApplicationUserId")]
  [ValidateNever]
  public ApplicationUser ApplicationUser { get; set; }
}
