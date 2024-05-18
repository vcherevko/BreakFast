using BreakFast.Ordering.Domain.ValueObjects;

namespace BreakFast.Ordering.Domain.Entities;

public class Order
{
	public int Id { get; set; }

	public int ConsumerId { get; set; }

	public int RestaurantId { get; set; }

	public int? CourierId { get; set; }

	public OrderStatus Status { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime ChangedAt { get; set; }

	public long Timestamp { get; set; }

	public required ICollection<OrderItem> OrderItems { get; set; }
}
