namespace DataAccess.Repository.IRepository;

using Models;

public interface ICoverTypeRepository : IRepository<CoverType> {
  void Update(CoverType coverType);
}
