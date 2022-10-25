namespace DataAccess.Repository;

using IRepository;
using Models;
using Data;
public class ProductRepository : Repository<Product>, IProductRepository {

  private ApplicationDbContext _db;

  public ProductRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(Product product) {
    _db.Products.Update(product);
  }
}
