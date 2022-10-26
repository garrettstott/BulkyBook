namespace DataAccess.Repository.IRepository;

using Models;

public interface IOrderDetailRepository : IRepository<OrderDetail> {
  void Update(OrderDetail orderDetail);
}
