using BreakFast.Ordering.API.DTOs;
using BreakFast.Ordering.Domain.Entities;
using BreakFast.Ordering.Domain.Exceptions;
using BreakFast.Ordering.Domain.Repositories;
using BreakFast.Ordering.Domain.ValueObjects;
using Mapster;

namespace BreakFast.Ordering.API.Services;

public class OrderService : IOrderService
{
	private readonly IRepositoryManager _repositoryManager;

	public OrderService(IRepositoryManager repositoryManager)
	{
		_repositoryManager = repositoryManager;
	}

	public async Task<OrderDto> GetOrderByIdAsync(int consumerId, int orderId, CancellationToken cancellationToken)
	{
		var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId, cancellationToken);
		ValidateOrderAccess(order, consumerId);
		return order.Adapt<OrderDto>();
	}

	public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int consumerId, CancellationToken cancellationToken)
	{
		var orders = await _repositoryManager.OrderRepository.GetOrdersAsync(consumerId, cancellationToken);
		var adaptedOrders = orders.Adapt<IEnumerable<OrderDto>>().ToList();
		adaptedOrders.ForEach(o =>
		{
			o.TotalPrice = o.OrderItems.Sum(oi => oi.Price * oi.Quantity);
		});

		return adaptedOrders;
	}

	public async Task CreateOrderAsync(int consumerId, OrderCreatingDto orderModel, CancellationToken cancellationToken)
	{
		var order = orderModel.Adapt<Order>();
		order.ConsumerId = consumerId;
		order.Status = OrderStatus.ConsumerCreated;

		_repositoryManager.OrderRepository.Add(order);
		await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
	}

	private static void ValidateOrderAccess(Order? order, int consumerId)
	{
		if (order is null)
		{
			throw new OrderNotFoundException();
		}

		if (order.ConsumerId != consumerId)
		{
			throw new OrderAccessDeniedException();
		}
	}
}
