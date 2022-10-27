namespace DataAccess.DbInitializer;

using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Utility;

public class DbInitializer : IDbInitializer {
  private readonly UserManager<IdentityUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly ApplicationDbContext _db;

  public DbInitializer(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext db
  ) {
    _roleManager = roleManager;
    _userManager = userManager;
    _db = db;
  }
  public void Initialize() {
    try {
      if (_db.Database.GetPendingMigrations().Any()) {
        _db.Database.Migrate();
      }
    }
    catch (Exception ex) {}
    if (!_roleManager.RoleExistsAsync(SD.ROLE_USER).GetAwaiter().GetResult()){
        _roleManager.CreateAsync(new IdentityRole(SD.ROLE_USER)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.ROLE_ADMIN)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.ROLE_COMPANY)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.ROLE_EMPLOYEE)).GetAwaiter().GetResult();
      _userManager.CreateAsync(new ApplicationUser {
        UserName = "garrett@test.com",
        Email = "garrett@test.com ",
        Name = "Garrett Stott",
        PhoneNumber = "5551235555",
        StreetAddress = "123 F St",
        City = "Seattle",
        State = "WA",
        PostalCode = "55555",
      }, "GQ6@WJd2DfCW3HaxN@&ZF").GetAwaiter().GetResult();
  
      ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "garrett@test.com");
      _userManager.AddToRoleAsync(user, SD.ROLE_ADMIN).GetAwaiter().GetResult();
    }
    return;
  }
}
