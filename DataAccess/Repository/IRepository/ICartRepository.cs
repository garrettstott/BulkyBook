namespace DataAccess.Repository.IRepository;

using Models;

public interface ICartRepository : IRepository<Cart> {
  int UpdateCount(Cart cart, int number);
}
