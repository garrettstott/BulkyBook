namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class CartRepository : Repository<Cart>, ICartRepository {

  private ApplicationDbContext _db;

  public CartRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public int UpdateCount(Cart cart, int number) {
    cart.Count = number;
    return cart.Count;
  }
}
