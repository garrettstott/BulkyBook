using DataAccess.Repository.IRepository;
using DataAccess.Data;
namespace DataAccess.Repository;

using Models;

public class UnitOfWork : IUnitOfWork {
  private ApplicationDbContext _db;

  public UnitOfWork(ApplicationDbContext db) {
    _db = db;
    Category = new CategoryRepository(_db);
    CoverType = new CoverTypeRepository(_db);
    Product = new ProductRepository(_db);
    Company = new CompanyRepository(_db);
    ApplicationUser = new ApplicationUserRepository(_db);
    OrderDetail = new OrderDetailRepository(_db);
    OrderHeader = new OrderHeaderRepository(_db);
    Cart = new CartRepository(_db);
  }
  public ICategoryRepository Category { get; private set; }
  public ICoverTypeRepository CoverType { get; private set; }
  public IProductRepository Product { get; private set; }
  public ICompanyRepository Company { get; private set; }
  public ICartRepository Cart { get; private set; }
  public IApplicationUserRepository ApplicationUser { get; private set; }
  public IOrderHeaderRepository OrderHeader { get; private set; }
  public IOrderDetailRepository OrderDetail{ get; set; }
  public void Save() {
    _db.SaveChanges();
  }
}
