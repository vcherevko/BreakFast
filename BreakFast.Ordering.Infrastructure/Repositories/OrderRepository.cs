using BreakFast.Ordering.Domain.Entities;
using BreakFast.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly OrderingContext _dbContext;

	public OrderRepository(OrderingContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Add(Order order)
	{
		order.ChangedAt = DateTime.UtcNow;
		order.CreatedAt = DateTime.UtcNow;
		_dbContext.Order.Add(order);
	}

	public async Task<IEnumerable<Order>> GetOrdersAsync(int consumerId, CancellationToken cancellationToken)
	{
		return await _dbContext.Order
			.AsNoTracking()
			.Where(o => o.ConsumerId == consumerId)
			.Include(o => o.OrderItems)
			.ToListAsync(cancellationToken);
	}

	public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken)
	{
		return await _dbContext.Order
			.AsNoTracking()
			.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
	}
}
