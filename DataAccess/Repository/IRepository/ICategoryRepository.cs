namespace DataAccess.Repository.IRepository;

using Models;

public interface ICategoryRepository : IRepository<Category> {
  void Update(Category category);
}
