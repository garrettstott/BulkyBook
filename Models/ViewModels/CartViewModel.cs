namespace Models.ViewModels; 

public class CartViewModel {
  public IList<Cart> ListCart { get; set; }
  public OrderHeader OrderHeader { get; set; }
}
