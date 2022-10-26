namespace DataAccess.Repository.IRepository;

using Models;

public interface IApplicationUserRepository : IRepository<ApplicationUser> {
  void Update(ApplicationUser applicationUser);
}
