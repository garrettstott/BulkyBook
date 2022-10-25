namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class CompanyRepository : Repository<Company>, ICompanyRepository {

  private ApplicationDbContext _db;

  public CompanyRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(Company company) {
    _db.Companies.Update(company);
  }
}
