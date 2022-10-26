namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository {

  private ApplicationDbContext _db;

  public ApplicationUserRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(ApplicationUser applicationUser) {
    _db.ApplicationUsers.Update(applicationUser);
  }
}


