namespace DataAccess.Repository;

using Data;
using IRepository;
using Models;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository {

  private ApplicationDbContext _db;

  public OrderHeaderRepository(ApplicationDbContext db) : base(db) {
    _db = db;
  }
  public void Update(OrderHeader orderHeader) {
    _db.OrderHeaders.Update(orderHeader);
  }
  public void UpdateStatus(int id, string orderStatus) {
    var _existingOrderHeader = _db.OrderHeaders.FirstOrDefault(oh => oh.Id == id);
    _existingOrderHeader.OrderStatus = orderStatus;
  }
}
