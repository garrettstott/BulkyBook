namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository {

  private ApplicationDbContext _db;

  public CoverTypeRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(CoverType coverType) {
    _db.CoverTypes.Update(coverType);
  }
}
