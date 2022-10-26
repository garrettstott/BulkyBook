namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository {

  private ApplicationDbContext _db;

  public OrderDetailRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(OrderDetail orderDetail) {
    _db.OrderDetails.Update(orderDetail);
  }
}
