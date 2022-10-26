namespace DataAccess.Repository.IRepository;

using Models;

public interface IOrderHeaderRepository : IRepository<OrderHeader> {
  void Update(OrderHeader orderHeader);
  void UpdateStatus(int id, string orderStatus);
}
