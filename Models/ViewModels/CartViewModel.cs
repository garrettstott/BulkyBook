namespace Models.ViewModels; 

public class CartViewModel {
  public IEnumerable<Cart> ListCart { get; set; }
  public double Total { get; set; }
}
