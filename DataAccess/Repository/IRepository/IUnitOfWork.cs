namespace DataAccess.Repository.IRepository; 

public interface IUnitOfWork {
  ICategoryRepository Category { get; }
  ICoverTypeRepository CoverType { get; }
  IProductRepository Product { get; }
  ICompanyRepository Company { get; }
  ICartRepository Cart { get; }
  IApplicationUserRepository ApplicationUser { get; }
  IOrderDetailRepository OrderDetail { get; }
  IOrderHeaderRepository OrderHeader { get; }
  void Save();
}

