using System.ComponentModel.DataAnnotations;

namespace Models;

using System.ComponentModel;

public class Category
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [DisplayName("Display Order")]
  [Range(1,100, ErrorMessage = "Must be between 1-100")]
  public int DisplayOrder { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}