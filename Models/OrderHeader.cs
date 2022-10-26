namespace Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderHeader {
  [Key]
  public int Id { get; set; }
  
  [Required]
  public string ApplicationUserId { get; set; }
  [ValidateNever]
  [ForeignKey("ApplicationUserId")]
  public ApplicationUser ApplicationUser { get; set; }
  
  [Required]
  public DateTime OrderDate { get; set; }
  public DateTime ShippingDate { get; set; }
  public double OrderTotal { get; set; }
  public string OrderStatus { get; set; } = "Pending";
  public string? TrackingNumber { get; set; }
  public string? SessionId { get; set; }
  public string? PaymentId { get; set; }
  public string? Name { get; set; }
  public string? StreetAddress { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? PostalCode { get; set; }
  public string? PhoneNumber { get; set; }
}
