namespace DataAccess.Repository.IRepository;

using Models;

public interface ICompanyRepository : IRepository<Company> {
  void Update(Company company);
}
