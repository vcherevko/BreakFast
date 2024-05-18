using BreakFast.Ordering.API.DTOs;

namespace BreakFast.Ordering.API.Services;

public interface IOrderService
{
	Task<OrderDto> GetOrderByIdAsync(int consumerId, int orderId, CancellationToken cancellationToken);

	Task<IEnumerable<OrderDto>> GetOrdersAsync(int consumerId, CancellationToken cancellationToken);

	Task CreateOrderAsync(int consumerId, OrderCreatingDto order, CancellationToken cancellationToken);
}
