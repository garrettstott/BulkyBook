namespace DataAccess.Repository.IRepository;

using Models;

public interface IProductRepository : IRepository<Product> {
  void Update(Product product);
}
