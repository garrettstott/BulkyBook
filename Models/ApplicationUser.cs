namespace Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationUser : IdentityUser {
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public string? StreetAddress { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? PostalCode { get; set; }
  public int? CompanyId { get; set; }
  [ForeignKey("CompanyId")]
  [ValidateNever]
  public Company Company { get; set; }
}
