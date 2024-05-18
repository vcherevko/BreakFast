using BreakFast.Ordering.Domain.Entities;

namespace BreakFast.Ordering.Domain.Repositories;

public interface IOrderRepository
{
	void Add(Order order);

	Task<IEnumerable<Order>> GetOrdersAsync(int consumerId, CancellationToken cancellationToken);

	Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken);
}
